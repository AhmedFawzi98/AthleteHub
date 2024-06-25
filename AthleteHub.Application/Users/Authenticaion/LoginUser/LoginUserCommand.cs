using AthleteHub.Application.Users.Dtos;
using MediatR;

namespace AthleteHub.Application.Users.Authenticaion.LoginUser;

public class LoginUserCommand : IRequest<UserLoginResponseDto>
{
    public string UserNameOrEmail { get; set; }
    public string Password { get; set; }
}
