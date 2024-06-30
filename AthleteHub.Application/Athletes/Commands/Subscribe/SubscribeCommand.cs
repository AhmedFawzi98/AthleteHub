using AthleteHub.Application.Athletes.Dtos;
using MediatR;

namespace AthleteHub.Application.Athletes.Commands.Subscribe
{
    public class SubscribeCommand : IRequest<SubscribeResponseDto>
    {
        public string SessionId { get; init; }
    }
}
