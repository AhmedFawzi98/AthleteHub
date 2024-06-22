namespace AthleteHub.Domain.Entities;

public class Measurement
{
    public int AthleteId { get; set; }
    public DateOnly Date {  get; set; }
    public decimal WeightInKg { get; set; }
    public decimal? BodyFatPercentage { get; set; }
    public decimal? BMI { get; set; }
    public int? BenchPressWeight { get; set; }
    public int? SquatWeight { get; set; }
    public int? DeadliftWeight { get; set; }
    public virtual Athlete Athlete { get; set; }
}
