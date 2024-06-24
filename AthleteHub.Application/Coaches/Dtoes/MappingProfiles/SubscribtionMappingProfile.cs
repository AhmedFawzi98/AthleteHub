using AthleteHub.Domain.Entities;
using AutoMapper;


namespace AthleteHub.Application.Coaches.Dtoes.MappingProfiles
{
    public class SubscribtionMappingProfile : Profile
    {
        public SubscribtionMappingProfile()
        {
            CreateMap<Subscribtion, SubscribtionDto>()
                .ForMember(d => d.SubscribtionsFeatures, opt => opt.MapFrom(src => src.SubscribtionsFeatures));
        }

    }
}
