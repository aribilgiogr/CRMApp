using AutoMapper;
using Core.Abstracts;
using Core.Abstracts.IServices;

namespace Business.Services
{
    public class ContactService(IUnitOfWork unitOfWork, IMapper mapper) : IContactService { }
}
