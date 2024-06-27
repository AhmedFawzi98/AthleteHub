using AthleteHub.Application.Users.Dtos;
using MediatR;

namespace AthleteHub.Application.Users.ChangeEmail.InitiateChangeEmail;

public class ChangeEmailCommand : IRequest<EmailConfirmationResponseDto>
{
    public string CurrentEmail { get; set; }
    public string NewEmail { get; set; }
    public string ConfirmNewEmail { get; set; }
    public string ClientEmailChangingConfirmationUrl { get; set; }
}
