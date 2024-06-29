using AthleteHub.Application.Coaches.Dtoes;
using AthleteHub.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthleteHub.Application.Athletes.Dtos.MappingProfiles
{
    public class AthleteMappingProfile:Profile
    {
        public AthleteMappingProfile()
        {
            CreateMap<Athlete, AthleteDto>()
                .ForMember(d => d.FirstName, opt => opt.MapFrom(src => src.ApplicationUser == null ? null : src.ApplicationUser.FirstName))
                .ForMember(d => d.LastName, opt => opt.MapFrom(src => src.ApplicationUser == null ? null : src.ApplicationUser.LastName))
                .ForMember(d => d.Gender, opt => opt.MapFrom(src => src.ApplicationUser == null ? default : src.ApplicationUser.Gender))
                .ForMember(d => d.DateOfBirth, opt => opt.MapFrom(src => src.ApplicationUser == null ? default : src.ApplicationUser.DateOfBirth))
                .ForMember(d => d.ProfilePicture, opt => opt.MapFrom(src => src.ApplicationUser == null ? default : src.ApplicationUser.ProfilePicture))
                .ForMember(d => d.Bio, opt => opt.MapFrom(src => src.ApplicationUser == null ? default : src.ApplicationUser.Bio))
                .ReverseMap();
        }
        
    }
}
