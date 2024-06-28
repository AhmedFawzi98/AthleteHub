using MediatR;

namespace AthleteHub.Application.Users.ResetPassword.ConfirmResetPassword;

public class ConfirmResetPasswordCommand:IRequest
{
    public string Token { get; set; }
    public string Email { get; set; }
    public string NewPassword { get; set; }
    public string ConfirmNewPassword { get; set; }
}
