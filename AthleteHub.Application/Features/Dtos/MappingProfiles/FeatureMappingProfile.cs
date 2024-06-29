using AthleteHub.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthleteHub.Application.Features.Dtos.MappingProfiles
{
    public class FeatureMappingProfile:Profile
    {
        public FeatureMappingProfile()
        {
            CreateMap<Feature, FeatureDto>();
        }
    }
}
