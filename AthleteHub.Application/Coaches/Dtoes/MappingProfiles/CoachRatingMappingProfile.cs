using AthleteHub.Domain.Entities;
using AutoMapper;


namespace AthleteHub.Application.Coaches.Dtoes.MappingProfiles
{
    public class CoachRatingMappingProfile : Profile
    {
        public CoachRatingMappingProfile()
        {
            CreateMap<CoachRating, CoachRatingDto>()
                .ForMember(d => d.AthleteFirstName, opt => opt.MapFrom(src => src.Athlete.ApplicationUser == null ? null : src.Athlete.ApplicationUser.FirstName))
                .ForMember(d => d.AthleteLastName, opt => opt.MapFrom(src => src.Athlete.ApplicationUser == null ? null : src.Athlete.ApplicationUser.LastName))
                .ForMember(d => d.AthleteProfilePicture, opt => opt.MapFrom(src => src.Athlete.ApplicationUser == null ? null : src.Athlete.ApplicationUser.ProfilePicture));
        }
    }
}
