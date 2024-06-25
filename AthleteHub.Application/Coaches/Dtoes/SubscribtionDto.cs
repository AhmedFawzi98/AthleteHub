using AthleteHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthleteHub.Application.Coaches.Dtoes
{
    public class SubscribtionDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal price { get; set; }
        public int DurationInMonths { get; set; }
        public virtual ICollection<SubscribtionFeatureDto> SubscribtionsFeatures { get; set; } = new List<SubscribtionFeatureDto>();
    }
}
