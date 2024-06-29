using AthleteHub.Application.Athletes.Dtos;
using MediatR;

namespace AthleteHub.Application.Athletes.Commands.CheckSubscribeAblity
{
    public class CheckSubscribeAblityCommand:IRequest<CheckSubscribeResponseDto>
    {
        public int CoachId { get; init; }
        public int SubscribtionId { get; init; }
    }
}
