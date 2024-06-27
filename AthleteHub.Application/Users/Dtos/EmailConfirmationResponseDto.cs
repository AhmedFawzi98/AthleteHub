using AthleteHub.Domain.Enums;

namespace AthleteHub.Application.Users.Dtos;

public class EmailConfirmationResponseDto
{
    public string UserEmailToConfirm {  get; set; }
    public string EmailConfirmationLink {  get; set; }
    public List<string> Roles { get; set; }
}