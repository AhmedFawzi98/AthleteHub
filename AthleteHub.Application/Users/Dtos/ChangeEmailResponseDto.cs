using AthleteHub.Domain.Enums;

namespace AthleteHub.Application.Users.Dtos;

public class ChangeEmailResponseDto
{
    public string UserOldEmail { get; set; }
    public string UserEmailToConfirm {  get; set; }
    public string EmailConfirmationLink {  get; set; }
}