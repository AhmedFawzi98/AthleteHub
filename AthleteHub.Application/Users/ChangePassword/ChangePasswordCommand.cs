using MediatR;

namespace AthleteHub.Application.Users.ChangePassword;

public class ChangePasswordCommand : IRequest
{
    public string CurrentPassword { get; set; }
    public string NewPassword { get; set; }
    public string ConfirmNewPassword { get; set; }
}
