namespace AthleteHub.Domain.Entities;

public class AthleteFavouriteCoach
{
    public int AthleteId {  get; set; }
    public int CoachId { get; set; }
    public virtual Athlete Athlete { get; set; }
    public virtual Coach Coach { get; set; }
}
