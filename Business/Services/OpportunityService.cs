using AutoMapper;
using Core.Abstracts;
using Core.Abstracts.IServices;

namespace Business.Services
{
    public class OpportunityService(IUnitOfWork unitOfWork, IMapper mapper) : IOpportunityService { }
}
