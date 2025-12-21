using AutoMapper;
using Core.Concretes.DTOs;
using Core.Concretes.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Profiles
{
    public class AccountProfiles: Profile
    {
        public AccountProfiles()
        {
            CreateMap<RegisterDTO, ApplicationUser>();
        }
    }
}
