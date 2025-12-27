using AutoMapper;
using Core.Abstracts;
using Core.Abstracts.IServices;
using Core.Concretes.DTOs;
using Core.Concretes.Entities;
using Microsoft.AspNetCore.Http;
using Utilities.Constants;
using Utilities.Results;

namespace Business.Services
{
    public class LeadService(IUnitOfWork unitOfWork, IMapper mapper) : ILeadService
    {
        public async Task<IResult> AddLeadAsync(LeadCreateDTO model)
        {
            try
            {
                var lead = mapper.Map<Lead>(model);
                await unitOfWork.LeadRepository.CreateAsync(lead);
                await unitOfWork.CommitAsync();
                return new SuccessResult("Lead" + Messages.AddedSuffix);
            }
            catch (Exception ex)
            {
                return new ErrorResult(Messages.ErrorOccurred + ": " + ex.Message);
            }
        }

        public async Task<IDataResult<IEnumerable<LeadListDTO>>> GetAllAsync(string? uid)
        {
            try
            {
                var leads = await unitOfWork.LeadRepository.FindManyAsync(x => string.IsNullOrEmpty(uid) || x.AssignedSalesPersonId == uid);
                var leadDTOs = mapper.Map<IEnumerable<LeadListDTO>>(leads);
                return new SuccessDataResult<IEnumerable<LeadListDTO>>(leadDTOs, "Leads" + Messages.RetrievedSuffix);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<IEnumerable<LeadListDTO>>(Messages.ErrorOccurred + ": " + ex.Message);
            }
        }

        private Task<IDataResult<IEnumerable<Lead>>> importCsvAsync(StreamReader? reader) => default;
        private Task<IDataResult<IEnumerable<Lead>>> importJsonAsync(StreamReader? reader) => default;
        private Task<IDataResult<IEnumerable<Lead>>> importExcelAsync(StreamReader? reader) => default;

        public async Task<IResult> ImportLeadsAsync(IFormFile file, string ext)
        {
            try
            {
                using var stream = file.OpenReadStream();
                using var reader = new StreamReader(stream);

                var result = ext switch
                {
                    ".csv" => await importCsvAsync(reader),
                    ".json" => await importJsonAsync(reader),
                    ".xlsx" => await importExcelAsync(reader),
                    _ => new ErrorDataResult<IEnumerable<Lead>>("Just .csv, .xlsx, .json allowed!")
                };

                if (result.Success)
                {
                    await unitOfWork.LeadRepository.CreateManyAsync(result.Data);
                    await unitOfWork.CommitAsync();
                    return new SuccessResult(result.Message);
                }
                else
                {
                    return new ErrorResult(result.Message);
                }
            }
            catch (Exception ex)
            {
                return new ErrorResult(ex.Message);
            }
        }
    }
}
