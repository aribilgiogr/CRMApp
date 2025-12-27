using AutoMapper;
using Core.Concretes.DTOs;
using Core.Concretes.Entities;

namespace Business.Profiles
{
    public class LeadProfiles : Profile
    {
        public LeadProfiles()
        {
            CreateMap<Lead, LeadListDTO>();
            CreateMap<LeadCreateDTO, Lead>();
        }
    }
}
