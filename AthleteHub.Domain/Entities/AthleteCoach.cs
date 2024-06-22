namespace AthleteHub.Domain.Entities;

public class AthleteCoach
{
    public int AthleteId {  get; set; }
    public int CoachId { get; set; }
    public virtual Athlete Athlete { get; set; }
    public virtual Coach Coach { get; set; }
    public bool IsCurrentlySubscribed { get; set; }
}
