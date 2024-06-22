namespace AthleteHub.Domain.Entities;

public class CoachRating
{
    public int AthleteId {  get; set; }
    public int CoachId { get; set; }
    public virtual Athlete Athlete { get; set; }
    public virtual Coach Coach { get; set; }
    public int Rate {  get; set; }
    public string Comment { get; set; }
}
