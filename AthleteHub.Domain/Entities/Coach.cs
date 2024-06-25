namespace AthleteHub.Domain.Entities;

public class Coach
{
    public int Id { get; set; }
    public string ApplicationUserId { get; set; }
    public virtual ApplicationUser ApplicationUser { get; set; }
    public string? Certificate { get; set; }
    public bool IsApproved { get; set; }
    public bool IsSuspended { get; set; }
    public int? RatingsCount {  get; set; }
    public Decimal? OverallRating { get; set;}
    public virtual ICollection<AthleteCoach> AthleteCoaches { get; set; } = new List<AthleteCoach>();
    public virtual ICollection<Content> Contents { get; set; } = new List<Content>();
    public virtual ICollection<Subscribtion> Subscribtions { get; set; } = new List<Subscribtion>();
    public virtual ICollection<AthleteFavouriteCoach> AthletesFavouriteCoaches { get; set; } = new List<AthleteFavouriteCoach>();
    public virtual ICollection<CoachRating> CoachesRatings { get; set; } = new List<CoachRating>();
    public virtual ICollection<CoachBlockedAthlete> CoachesBlockedAthletees { get; set; } = new List<CoachBlockedAthlete>();
    

}
