using AthleteHub.Application.Subscribtions.Dtos;
using AthleteHub.Domain.Enums;
using System.Text.Json.Serialization;

namespace AthleteHub.Application.Coaches.Dtoes
{
    public class CoachDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public DateOnly DateOfBirth { get; set; }
        [JsonIgnore]
        public string ProfilePicture { get; set; }
        public string? SasProfilePicture { get; set; }
        [JsonIgnore]
        public string? Certificate { get; set; }
        public string? SasCertificate { get; set; }
        public string? Bio { get; set; }
        public int? RatingsCount { get; set; }
        public Decimal? OverallRating { get; set; }

        
        public virtual ICollection<SubscribtionDto> Subscribtions { get; set; } = new List<SubscribtionDto>();
        public virtual ICollection<CoachRatingDto> CoachesRatings { get; set; }
    }
}
