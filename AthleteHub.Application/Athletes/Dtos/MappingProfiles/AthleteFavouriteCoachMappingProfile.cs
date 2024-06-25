

using AthleteHub.Domain.Entities;
using AutoMapper;

namespace AthleteHub.Application.Athletes.Dtos.MappingProfiles
{
    public class AthleteFavouriteCoachMappingProfile:Profile
    {
        public AthleteFavouriteCoachMappingProfile()
        {
            CreateMap<AthleteFavouriteCoach, AthleteFavouriteCoachDto>();
        }
    }
}
