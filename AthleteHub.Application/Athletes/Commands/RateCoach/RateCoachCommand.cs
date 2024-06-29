
using AthleteHub.Application.Athletes.Dtos;
using MediatR;

namespace AthleteHub.Application.Athletes.Commands.RateCoach
{
    public class RateCoachCommand:IRequest<RateCoachResponseDto>
    {
        public int CoachId { get; set; }
        public int Rate {  get; init; }
        public string Comment {  get; init; }
    }
}
