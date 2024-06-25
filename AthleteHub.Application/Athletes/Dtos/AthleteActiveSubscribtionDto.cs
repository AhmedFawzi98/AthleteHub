

namespace AthleteHub.Application.Athletes.Dtos
{
    public class AthleteActiveSubscribtionDto
    {
        public int AthleteId { get; set; }
        public int SubscribtionId { get; set; }
        public DateOnly? SubscribtionStartDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public DateOnly? SubscribtionEndDate { get; set; }
    }
}
