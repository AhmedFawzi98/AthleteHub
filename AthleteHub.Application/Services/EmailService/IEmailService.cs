namespace AthleteHub.Application.Services.EmailService;

public interface IEmailService
{
    Task SendEmailAsync(Message message);
}
