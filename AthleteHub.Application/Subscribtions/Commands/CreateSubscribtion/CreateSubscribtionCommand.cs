using AthleteHub.Application.SubscribtionFeatures.Dtos;
using AthleteHub.Application.Subscribtions.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthleteHub.Application.Subscribtions.Commands.CreateSubscribtion
{
    public class CreateSubscribtionCommand : IRequest<SubscribtionDto>
    {
        public string Name { get; set; }
        public decimal price { get; set; }
        public int DurationInMonths { get; set; }
        public List<SubscribtionFeatureDto> SubscribtionFeatures { get; set; } = new List<SubscribtionFeatureDto>();
    }
}
