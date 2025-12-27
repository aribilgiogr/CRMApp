using AutoMapper;
using Core.Abstracts;
using Core.Abstracts.IServices;
using Core.Concretes.DTOs;
using Core.Concretes.Entities;
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
    }
}
