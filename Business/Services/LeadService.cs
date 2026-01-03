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
using System.Text.Json.Serialization;

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
                IEnumerable<Lead> leads;
                if (string.IsNullOrEmpty(uid))
                {
                    leads = await unitOfWork.LeadRepository.FindManyAsync();
                }
                else
                {
                    leads = await unitOfWork.LeadRepository.FindManyAsync(x => x.AssignedSalesPersonId == uid || x.AssignedSalesPersonId == null);
                }
                var leadDTOs = mapper.Map<IEnumerable<LeadListDTO>>(leads);
                return new SuccessDataResult<IEnumerable<LeadListDTO>>(leadDTOs, "Leads" + Messages.RetrievedSuffix);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<IEnumerable<LeadListDTO>>(Messages.ErrorOccurred + ": " + ex.Message);
            }
        }

        private async Task<IDataResult<IEnumerable<LeadCreateDTO>>> importCsvAsync(StreamReader reader)
        {
            try
            {
                using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture));
                var records = await csv.GetRecordsAsync<LeadCreateDTO>().ToEnumerableAsync();
                return new SuccessDataResult<IEnumerable<LeadCreateDTO>>(records, "Import success.");
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<IEnumerable<LeadCreateDTO>>("Import failed. " + ex.Message);
            }
        }
        private async Task<IDataResult<IEnumerable<LeadCreateDTO>>> importJsonAsync(Stream stream)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter() }
            };
            try
            {
                var data = await JsonSerializer.DeserializeAsync<IEnumerable<LeadCreateDTO>>(stream, options);
                if (data == null)
                {
                    return new ErrorDataResult<IEnumerable<LeadCreateDTO>>("Import failed.");
                }
                else
                {
                    return new SuccessDataResult<IEnumerable<LeadCreateDTO>>(data, "Import success.");
                }
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<IEnumerable<LeadCreateDTO>>("Import failed. " + ex.Message);
            }
        }
        private async Task<IDataResult<IEnumerable<LeadCreateDTO>>> importExcelAsync(Stream stream, string? sheetName = null, string startCell = "A1")
        {
            try
            {
                var data = await stream.QueryAsync<LeadCreateDTO>(sheetName: sheetName, startCell: startCell);
                return new SuccessDataResult<IEnumerable<LeadCreateDTO>>(data, "Import success.");
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<IEnumerable<LeadCreateDTO>>("Import failed. " + ex.Message);
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
                    return new ErrorResult("File corrupt!");
                }

                var result = ext switch
                {
                    ".csv" => await importCsvAsync(reader),
                    ".json" => await importJsonAsync(stream),
                    ".xlsx" => await importExcelAsync(stream),
                    _ => new ErrorDataResult<IEnumerable<LeadCreateDTO>>("Just .csv, .xlsx, .json allowed!")
                };

                if (result.Success)
                {
                    var leads = mapper.Map<IEnumerable<Lead>>(result.Data);
                    await unitOfWork.LeadRepository.CreateManyAsync(leads);
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

        public async Task<IResult> AssignLeadToSalesPersonAsync(string userId, int leadId)
        {
            try
            {
                var lead = await unitOfWork.LeadRepository.FindByIdAsync(leadId);
                if (lead != null)
                {
                    lead.AssignedSalesPersonId = userId;
                    await unitOfWork.LeadRepository.UpdateAsync(lead);
                    await unitOfWork.CommitAsync();
                    return new SuccessResult("Lead" + Messages.UpdatedSuffix);
                }
                else
                {
                    return new ErrorResult("Lead" + Messages.NotFoundSuffix);
                }

            }
            catch (Exception ex)
            {
                return new ErrorResult(Messages.ErrorOccurred + ": " + ex.Message);
            }
        }
    }
}
