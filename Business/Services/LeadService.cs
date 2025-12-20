using AutoMapper;
using Core.Abstracts;
using Core.Abstracts.IServices;
using Core.Concretes.DTOs;
using Utilities.Constants;
using Utilities.Results;

namespace Business.Services
{
    public class LeadService(IUnitOfWork unitOfWork, IMapper mapper) : ILeadService
    {
        public async Task<IDataResult<IEnumerable<LeadListDTO>>> GetAllAsync()
        {
            try
            {
                var leads = await unitOfWork.LeadRepository.FindManyAsync();
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
