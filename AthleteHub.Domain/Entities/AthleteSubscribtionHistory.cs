namespace AthleteHub.Domain.Entities;

public class AthleteSubscribtionHistory
{
    public int Id { get; set; }
    public int AthleteId { get; set; }
    public int SubscribtionId { get; set; }
    public DateOnly? SubscribtionStartDate { get; set; }
    public DateOnly? SubscribtionEndDate { get; set; } 
}
