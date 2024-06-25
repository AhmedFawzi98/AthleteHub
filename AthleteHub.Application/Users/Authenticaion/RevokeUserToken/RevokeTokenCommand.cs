using AthleteHub.Application.Users.Dtos;
using MediatR;

namespace AthleteHub.Application.Users.Authenticaion.RevokeUserToken;

public class RevokeTokenCommand : IRequest<RevokeTokenResponseDto>
{
}
