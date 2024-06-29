using AthleteHub.Application.Services.EmailService;
using AthleteHub.Infrastructure.Configurations;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MimeKit;

namespace AthleteHub.Infrastructure.EmailService;

internal class EmailService(IOptions<EmailSettings> emailsettingsOptions) : IEmailService
{
    private readonly EmailSettings _emailsettings = emailsettingsOptions.Value;

    public async Task SendEmailAsync(Message message)
    {
        var Email = new MimeMessage()
        {
            Sender = MailboxAddress.Parse(_emailsettings.From),
            Subject = message.Subject
        };
        Email.To.Add(MailboxAddress.Parse(message.MailTo));
        Email.From.Add(new MailboxAddress(_emailsettings.Username, _emailsettings.From));

        var builder = new BodyBuilder();

        await BuildAttachmentsAsync(builder, message);

        BuildBody(builder, message);

        Email.Body = builder.ToMessageBody();

        using (var smtp = new SmtpClient())
        {
            await smtp.ConnectAsync(_emailsettings.SmtpServer, _emailsettings.Port, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_emailsettings.From, _emailsettings.Password);
            await smtp.SendAsync(Email);
            await smtp.DisconnectAsync(true);
        }
    }

    private async Task BuildAttachmentsAsync(BodyBuilder builder, Message message)
    {
        if (message.Attachments is not null)
        {
            byte[] fileBytes;
            foreach (IFormFile attachment in message.Attachments)
            {
                if (attachment.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await attachment.CopyToAsync(memoryStream);
                        fileBytes = memoryStream.ToArray();
                        builder.Attachments.Add(attachment.FileName, fileBytes, ContentType.Parse(attachment.ContentType));
                    }
                }
            }
        }
    }
    private void BuildBody(BodyBuilder builder, Message message)
    {
        if (message.MessageBodyType == MessageBodyType.Html)
        {
            if (message.Link != null && message.LinkPlaceHolder != null)
                message.Body = message.Body.Replace(message.LinkPlaceHolder, message.Link);

            builder.HtmlBody = message.Body;
        }
        else
        {
            builder.TextBody = message.Body;
        }
    }
}
