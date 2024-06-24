using AthleteHub.Domain.Entities;
using AutoMapper;


namespace AthleteHub.Application.Coaches.Dtoes.MappingProfiles
{
    public class SubscribtionFeatureMappingProfile : Profile
    {
        public SubscribtionFeatureMappingProfile()
        {
            CreateMap<SubscribtionFeature, SubscribtionFeatureDto>()
                .ForMember(d => d.Name, opt => opt.MapFrom(src => src.Feature == null ? default : src.Feature.Name))
                .ForMember(d => d.Description, opt => opt.MapFrom(src => src.Feature == null ? default : src.Feature.Description));
        }
    }
}
