using AthleteHub.Domain.Entities;
using AutoMapper;


namespace AthleteHub.Application.Athletes.Dtos.MappingProfiles
{
    public class AthleteActiveSubscribtionMappingProfile:Profile
    {
        public AthleteActiveSubscribtionMappingProfile()
        {
            CreateMap<AthleteActiveSubscribtion, AthleteActiveSubscribtionDto>();
        }
    }
}
