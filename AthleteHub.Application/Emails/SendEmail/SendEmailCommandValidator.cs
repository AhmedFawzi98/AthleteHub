using AthleteHub.Application.Services.EmailService;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AthleteHub.Application.Emails.SendEmail;

public class SendEmailCommandValidator : AbstractValidator<SendEmailCommand>
{
    public SendEmailCommandValidator()
    {
        RuleFor(dto => dto.MailTo)
            .Matches(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$")
            .WithMessage("Enter a valid email address.");
    }
}
