using AutoMapper;
using Core.Abstracts;
using Core.Abstracts.IServices;
using Core.Concretes.DTOs;
using Core.Concretes.Entities;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Http;
using System.Globalization;
using Utilities.Constants;
using Utilities.Results;
using System.Linq;
using Utilities.Extensions;
using System.Text.Json;
using MiniExcelLibs;

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

        private async Task<IDataResult<IEnumerable<Lead>>> importCsvAsync(StreamReader reader)
        {
            try
            {
                using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture));
                var records = await csv.GetRecordsAsync<Lead>().ToEnumerableAsync();
                return new SuccessDataResult<IEnumerable<Lead>>(records, "Import success.");
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<IEnumerable<Lead>>("Import failed. " + ex.Message);
            }
        }
        private async Task<IDataResult<IEnumerable<Lead>>> importJsonAsync(Stream stream)
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true, };
            try
            {
                var data = await JsonSerializer.DeserializeAsync<IEnumerable<Lead>>(stream, options);
                if (data == null)
                {
                    return new ErrorDataResult<IEnumerable<Lead>>("Import failed.");
                }
                else
                {
                    return new SuccessDataResult<IEnumerable<Lead>>(data, "Import success.");
                }
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<IEnumerable<Lead>>("Import failed. " + ex.Message);
            }
        }
        private async Task<IDataResult<IEnumerable<Lead>>> importExcelAsync(Stream stream)
        {
            try
            {
                var data = await stream.QueryAsync<Lead>();
                return new SuccessDataResult<IEnumerable<Lead>>(data, "Import success.");
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<IEnumerable<Lead>>("Import failed. " + ex.Message);
            }
        }

        public async Task<IResult> ImportLeadsAsync(IFormFile file, string ext)
        {
            try
            {
                using var stream = file.OpenReadStream();
                using var reader = new StreamReader(stream);

                if (reader == null)
                {
                    return new ErrorDataResult<IEnumerable<Lead>>("File corrupt!");
                }

                var result = ext switch
                {
                    ".csv" => await importCsvAsync(reader),
                    ".json" => await importJsonAsync(stream),
                    ".xlsx" => await importExcelAsync(stream),
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
