using MediatR;

namespace AthleteHub.Application.Users.Authenticaion.ConfirmEmail;

public class ConfirmChangeEmailCommand : IRequest
{
    public string UserEmailToConfirm { get; set; }
    public string Token { get; set; }
}
