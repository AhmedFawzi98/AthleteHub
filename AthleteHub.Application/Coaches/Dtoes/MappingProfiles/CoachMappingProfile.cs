using AthleteHub.Domain.Entities;
using AthleteHub.Domain.Enums;
using AutoMapper;


namespace AthleteHub.Application.Coaches.Dtoes.MappingProfiles
{
    public class CoachMappingProfile : Profile
    {
        public CoachMappingProfile()
        {
            CreateMap<Coach, CoachDto>()
                .ForMember(d => d.FirstName, opt => opt.MapFrom(src => src.ApplicationUser == null ? null : src.ApplicationUser.FirstName))
                .ForMember(d => d.LastName, opt => opt.MapFrom(src => src.ApplicationUser == null ? null : src.ApplicationUser.LastName))
                .ForMember(d => d.Gender, opt => opt.MapFrom(src => src.ApplicationUser == null ? default : src.ApplicationUser.Gender))
                .ForMember(d => d.DateOfBirth, opt => opt.MapFrom(src => src.ApplicationUser == null ? default : src.ApplicationUser.DateOfBirth))
                .ForMember(d => d.ProfilePicture, opt => opt.MapFrom(src => src.ApplicationUser == null ? default : src.ApplicationUser.ProfilePicture))
                .ForMember(d => d.Bio, opt => opt.MapFrom(src => src.ApplicationUser == null ? default : src.ApplicationUser.Bio))
                .ForMember(d => d.Subscribtions, opt => opt.MapFrom(src => src.Subscribtions))
                .ForMember(d => d.CoachesRatings, opt => opt.MapFrom(src => src.CoachesRatings))
                .ReverseMap();
        }

    }
}
