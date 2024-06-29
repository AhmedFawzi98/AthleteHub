using AthleteHub.Application.Services.EmailService;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Net.Mail;

namespace AthleteHub.Application.Emails.SendEmail;

public class SendEmailCommandHandler(IEmailService _emailService) : IRequestHandler<SendEmailCommand>
{
    public async Task Handle(SendEmailCommand request, CancellationToken cancellationToken)
    {
        var message = new Message(request.MailTo, request.Subject, request.Body,
            request.MessageBodyType, request.Link!, request.LinkPlaceHolder, request.Attachments!);
        
        await _emailService.SendEmailAsync(message);
    }
}
