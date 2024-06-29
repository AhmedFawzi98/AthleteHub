using AthleteHub.Domain.Enums;

namespace AthleteHub.Application.Users.Dtos;

public class ChangeEmailResponseDto
{
    public string UserEmailToConfirm {  get; set; }
    public string EmailConfirmationLink {  get; set; }
}