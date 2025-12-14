using AutoMapper;
using Core.Abstracts;
using Core.Abstracts.IServices;

namespace Business.Services
{
    public class ActivityService(IUnitOfWork unitOfWork, IMapper mapper) : IActivityService { }
}
