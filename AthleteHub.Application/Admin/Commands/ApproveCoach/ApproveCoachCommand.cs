using AthleteHub.Application.Admin.Dtos;
using MediatR;
namespace AthleteHub.Application.Admin.Commands.ApproveCoach
{
    public class ApproveCoachCommand:IRequest<ApprovalResponseDto>
    {
        public int CoachId { get; init; }
    }
}
