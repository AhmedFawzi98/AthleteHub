namespace AthleteHub.Domain.Entities;

public class SubscribtionFeature
{
    public int SubscribtionId { get; set; }
    public int FeatureId { get; set; }
    public virtual Feature Feature { get; set; }
    public virtual Subscribtion Subscribtion { get; set; }
}
