using AutoMapper;
using Core.Abstracts;
using Core.Abstracts.IServices;

namespace Business.Services
{
    public class LeadService(IUnitOfWork unitOfWork, IMapper mapper) : ILeadService { }
}
