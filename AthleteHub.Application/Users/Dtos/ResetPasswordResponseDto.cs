namespace AthleteHub.Application.Users.Dtos;

public class ResetPasswordResponseDto
{
    public string UserEmailToConfirm {  get; set; }
    public string PasswordResetLink {  get; set; }
}