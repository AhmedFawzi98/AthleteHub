namespace AthleteHub.Domain.Entities;

public class Subscribtion
{
    public int Id { get; set; }
    public int CoachId { get; set; }
    public string Name { get; set; }
    public decimal price { get; set; }
    public int DurationInMonths { get; set; }
    public virtual Coach Coach { get; set; }
    public virtual ICollection<AthleteActiveSubscribtion> AthletesActiveSubscribtions { get; set; } = new List<AthleteActiveSubscribtion>();
    public virtual ICollection<SubscribtionFeature> SubscribtionsFeatures { get; set; } = new List<SubscribtionFeature>();

}
