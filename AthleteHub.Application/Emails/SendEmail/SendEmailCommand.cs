using AthleteHub.Application.Services.EmailService;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AthleteHub.Application.Emails.SendEmail;

public class SendEmailCommand : IRequest
{
    public string MailTo { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
    public List<IFormFile>? Attachments { get; set; }
    public MessageBodyType MessageBodyType { get; set; }
    public string? Link {  get; set; }
    public string? LinkPlaceHolder { get; set; }
}
