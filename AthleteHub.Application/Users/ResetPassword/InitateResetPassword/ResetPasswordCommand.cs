using AthleteHub.Application.Users.Dtos;
using MediatR;

namespace AthleteHub.Application.Users.ResetPassword.InitateResetPassword;

public class ResetPasswordCommand : IRequest<ResetPasswordResponseDto>
{
    public string Email { get; set; }
    public string ClientResetPasswordUrl { get; set; }
}
