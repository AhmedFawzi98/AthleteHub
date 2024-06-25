using AthleteHub.Application.Athletes.Dtos;
using MediatR;

namespace AthleteHub.Application.Athletes.Commands.Subscribe
{
    public class SubscribeCommand : IRequest<AthleteActiveSubscribtionDto>
    {
        public int AthleteId { get; init; }
        public int CoachId { get; init; }
        public int SubscribtionId { get; init; }
        public int SubscribtionDurationInMonth { get; init; }
        public decimal SubscribtionPrice { get; init; }
        public string SubscribtionName { get; init; }
    }
}
