using AthleteHub.Application.Users.Dtos;
using MediatR;

namespace AthleteHub.Application.Users.GetUser;

public class GetUserCommand : IRequest<UserDto>
{
    public string ApplicationUserId { get; init; }
}
