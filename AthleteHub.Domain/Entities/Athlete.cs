namespace AthleteHub.Domain.Entities;

public class Athlete
{
    public int Id { get; set; }
    public string ApplicationUserId { get; set; }
    public virtual ApplicationUser ApplicationUser { get; set; }
    public decimal HeightInCm { get; set; }
    public virtual ICollection<AthleteCoach> AthletesCoaches { get; set; } = new List<AthleteCoach>();
    public virtual ICollection<AthleteActiveSubscribtion> AthletesActiveSubscribtions { get; set; } = new List<AthleteActiveSubscribtion>();
    public virtual ICollection<Post> Posts { get; set; }
    public virtual ICollection<Measurement> Measurements { get; set; }
    public virtual ICollection<AthleteFavouriteCoach> AthletesFavouriteCoaches { get; set; }
    public virtual ICollection<CoachRating> CoachesRatings { get; set; }
    public virtual ICollection<CoachBlockedAthlete> CoachesBlockedAthletees { get; set; }

}
