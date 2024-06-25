

using AthleteHub.Application.Athletes.Dtos;
using MediatR;

namespace AthleteHub.Application.Athletes.Commands.AddToFavourite
{
    public class AddToFavouriteCommand:IRequest<AthleteFavouriteCoachDto>
    {
        public int AthleteId { get; init; }
        public int CoachId { get; init; }

    }
}
