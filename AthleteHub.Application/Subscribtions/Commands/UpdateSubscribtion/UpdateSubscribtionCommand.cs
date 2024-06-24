using AthleteHub.Application.SubscribtionFeatures.Dtos;
using AthleteHub.Application.Subscribtions.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthleteHub.Application.Subscribtions.Commands.UpdateSubscribtion
{
    public class UpdateSubscribtionCommand : IRequest<SubscribtionDto>
    {
        public int Id { get; private set; }
        public string Name { get; init; }
        public decimal price { get; init; }
        public int DurationInMonths { get; set; }
        public ICollection<SubscribtionFeatureDto> SubscribtionFeatures { get; set; }

        public void SetId(int id)
        {
            Id = id;
        }
    }
}
