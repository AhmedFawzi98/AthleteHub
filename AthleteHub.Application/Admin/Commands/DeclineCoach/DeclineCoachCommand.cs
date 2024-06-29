
using AthleteHub.Application.Admin.Dtos;
using MediatR;


namespace AthleteHub.Application.Admin.Commands.DeclineCoach
{
    public class DeclineCoachCommand : IRequest<ApprovalResponseDto>
    {
        public int CoachId { get; init; }
    }
}
