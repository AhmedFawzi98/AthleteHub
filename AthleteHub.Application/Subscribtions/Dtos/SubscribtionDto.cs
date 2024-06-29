using AthleteHub.Application.Coaches.Dtoes;

namespace AthleteHub.Application.Subscribtions.Dtos
{
    public class SubscribtionDto
    {
        public int Id { get; set; }
        public int CoachId { get; set; }
        public string Name { get; set; }
        public decimal price { get; set; }
        public int DurationInMonths { get; set; }
        public List<SubscribtionFeatureDto> SubscribtionsFeatures { get; set; } = new List<SubscribtionFeatureDto>();
    }
}
