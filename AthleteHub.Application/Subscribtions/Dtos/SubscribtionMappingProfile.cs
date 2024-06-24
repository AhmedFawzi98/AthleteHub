using AthleteHub.Application.Subscribtions.Commands.CreateSubscribtion;
using AthleteHub.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthleteHub.Application.Subscribtions.Dtos
{
    public class SubscribtionMappingProfile : Profile
    {
        public SubscribtionMappingProfile()
        {
            CreateMap<Subscribtion, SubscribtionDto>()
                .ForMember(d => d.SubscribtionFeatures, opt => opt.MapFrom(src => src.SubscribtionsFeatures))
                .ReverseMap();
        }
    }
}
