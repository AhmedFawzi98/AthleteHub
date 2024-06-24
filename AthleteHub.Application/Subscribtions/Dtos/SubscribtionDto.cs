using AthleteHub.Application.SubscribtionFeatures.Dtos;
using AthleteHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthleteHub.Application.Subscribtions.Dtos
{
    public class SubscribtionDto
    {
        public int Id { get; set; }
        public int CoachId { get; set; }
        public string Name { get; set; }
        public decimal price { get; set; }
        public int DurationInMonths { get; set; }
        public ICollection<SubscribtionFeatureDto> SubscribtionFeatures { get; set; } = new List<SubscribtionFeatureDto>();

        //public ICollection<AthleteActiveSubscribtionDto> AthleteActiveSubscribtions { get; set; } = new List<AthleteActiveSubscribtionDto>();

    }
}
