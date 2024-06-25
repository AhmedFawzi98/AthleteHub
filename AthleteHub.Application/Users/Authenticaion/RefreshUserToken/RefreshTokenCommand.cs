using AthleteHub.Application.Users.Dtos;
using MediatR;

namespace AthleteHub.Application.Users.Authenticaion.RefreshUserToken;

public class RefreshTokenCommand : IRequest<RefreshTokenResponseDto>
{
}
