namespace AthleteHub.Domain.Entities;

public class Feature
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public virtual ICollection<SubscribtionFeature> SubscribtionsFeatures { get; set; } = new List<SubscribtionFeature>();

}
