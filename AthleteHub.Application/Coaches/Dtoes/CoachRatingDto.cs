

namespace AthleteHub.Application.Coaches.Dtoes
{
    public class CoachRatingDto
    {
        public int AthleteId { get; set; }
        public int CoachId { get; set; }
        public string AthleteFirstName { get; set; }
        public string AthleteLastName { get; set; }
        public string? AthleteProfilePicture { get; set; }
        public int Rate { get; set; }
        public string Comment { get; set; }
    }
}
