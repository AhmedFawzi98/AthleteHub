using AthleteHub.Application.Coaches.Dtoes;
using MediatR;
using Resturants.Application.Common;


namespace AthleteHub.Application.Coaches.Queries.FindCoach
{
    public class FindCoachByIdQuery:IRequest<CoachDto>
    {
        public int Id { get; init; }
    }
}
