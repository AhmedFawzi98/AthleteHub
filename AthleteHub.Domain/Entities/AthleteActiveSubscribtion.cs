namespace AthleteHub.Domain.Entities;

public class AthleteActiveSubscribtion
{
    public int AthleteId { get; set; }
    public int SubscribtionId { get; set; }
    public DateOnly? SubscribtionStartDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    public DateOnly? SubscribtionEndDate { get; set; }
    public virtual Athlete Athlete {  get; set; }
    public virtual Subscribtion Subscribtion {  get; set; }
}
