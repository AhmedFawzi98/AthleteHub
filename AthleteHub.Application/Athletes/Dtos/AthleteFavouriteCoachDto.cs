

namespace AthleteHub.Application.Athletes.Dtos
{
    public class AthleteFavouriteCoachDto
    {
        public int AthleteId { get; set; }
        public int CoachId { get; set; }
        public bool CanAddToFavourite { get; set; } 
    }
}
