using AthleteHub.Application.Athletes.Commands.CreateMeasurement;
using AthleteHub.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthleteHub.Application.Athletes.Dtos
{
    public class MeasurementMappingProfile : Profile
    {
        public MeasurementMappingProfile() 
        {
            CreateMap<Measurement, MeasurementDto>().ReverseMap();

            CreateMap<CreateMeasurementCommand, Measurement>();


        }
    }
}
