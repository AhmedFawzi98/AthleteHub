namespace AthleteHub.Infrastructure.Configurations;

public class EmailSettings
{
    public const string EmailService = "EmailService";
    public string From { get; set; }
    public string Username { get; set; }
    public string SmtpServer { get; set; }
    public int Port { get; set; }
    public string Password { get; set; }
}
