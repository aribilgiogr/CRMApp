using AutoMapper;
using Core.Concretes.DTOs;
using Core.Concretes.Entities;

namespace Business.Profiles
{
    public class LeadProfiles : Profile
    {
        public LeadProfiles()
        {
            CreateMap<Lead, LeadListDTO>()
                .ForMember(dest => dest.AssignedSalesPersonFullName,
                            opt => opt.MapFrom(src => src.AssignedSalesPerson != null
                                                        ? $"{src.AssignedSalesPerson.FirstName} {src.AssignedSalesPerson.LastName}"
                                                        : null));
            CreateMap<LeadCreateDTO, Lead>();
        }
    }

    public class ActivityProfiles : Profile
    {
        public ActivityProfiles()
        {
            CreateMap<Activity, ActivityListDTO>().ForMember(dest => dest.AssignedSalesPersonName, opt => opt.MapFrom(src => src.AssignedSalesPerson != null ? $"{src.AssignedSalesPerson.FirstName} {src.AssignedSalesPerson.LastName}" : null));
        }
    }
}
