using MediatR;

namespace AthleteHub.Application.Users.Authenticaion.ConfirmEmail;

public class ConfirmEmailCommand : IRequest
{
    public string UserEmailToConfirm { get; set; }
    public string Token { get; set; }
}
