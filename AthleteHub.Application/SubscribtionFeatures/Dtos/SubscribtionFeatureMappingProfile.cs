using AthleteHub.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthleteHub.Application.SubscribtionFeatures.Dtos
{
    public class SubscribtionFeatureMappingProfile : Profile
    {
        public SubscribtionFeatureMappingProfile()
        {
            //CreateMap<CreateSubscribtionFeatureCommand, SubscribtionFeature>();

            CreateMap<SubscribtionFeature, SubscribtionFeatureDto>()
                .ForMember(d => d.FeatureName,opt=>opt.MapFrom(src=>src.Feature.Name)).ReverseMap() ;
        }
    }
}
