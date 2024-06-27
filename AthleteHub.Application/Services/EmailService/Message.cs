using Microsoft.AspNetCore.Http;

namespace AthleteHub.Application.Services.EmailService
{
    public class Message
    {
        public Message(string mailTo, string subject, string body, MessageBodyType messageBodyType,
            string? link = null, string? linkPlaceholder = null, List<IFormFile>? attachments = null)
        {
            MailTo = mailTo;
            Subject = subject;
            Body = body;
            Attachments = attachments;
            MessageBodyType = messageBodyType;
            Link = link;
            LinkPlaceHolder = linkPlaceholder;
        }

        public string MailTo { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public List<IFormFile>? Attachments { get; set; }
        public MessageBodyType MessageBodyType {  get; set; }
        public string? Link { get; set; }
        public string? LinkPlaceHolder { get; set; }
    }
}