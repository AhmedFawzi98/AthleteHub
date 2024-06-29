using AthleteHub.Application.Athletes.Dtos;
using MediatR;

namespace AthleteHub.Application.Athletes.Commands.Subscribe
{
    public class SubscribeCommand : IRequest<SubscribeResponseDto>
    {
        public int CoachId { get; init; }
        public int SubscribtionId { get; init; }
        public int SubscribtionDurationInMonth { get; init; }
        public decimal SubscribtionPrice { get; init; }
        public string SubscribtionName { get; init; }
        public string SessionId { get; init; }
    }
}
