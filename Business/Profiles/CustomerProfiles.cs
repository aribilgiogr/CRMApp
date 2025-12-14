using AutoMapper;
using Core.Concretes.DTOs;
using Core.Concretes.Entities;

namespace Business.Profiles
{
    public class CustomerProfiles : Profile
    {
        public CustomerProfiles()
        {
            CreateMap<Customer, CustomerListDTO>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.TotalOpportunities, opt => opt.MapFrom(src => src.Opportunities.Count))
                .ForMember(dest => dest.TotalValue, opt => opt.MapFrom(src => src.Opportunities.Sum(o => o.Value)));

            CreateMap<CreateCustomerDTO, Customer>();
            CreateMap<UpdateCustomerDTO, Customer>();
        }
    }
}
