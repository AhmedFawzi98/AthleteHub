using AthleteHub.Application.Subscribtions.Commands.CreateSubscribtion;
using AthleteHub.Application.Subscribtions.Commands.UpdateSubscribtion;
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
            CreateMap<UpdateSubscribtionCommand, Subscribtion>();
            CreateMap<CreateSubscribtionCommand, Subscribtion>()
                .ForMember(dest => dest.SubscribtionsFeatures, opt => opt.Ignore());
            CreateMap<Subscribtion, SubscribtionDto>();
        }
    }
}
