using AthleteHub.Application.Users.Dtos;
using FluentValidation;
using MediatR;

namespace AthleteHub.Application.Users.ChangeEmail.InitiateChangeEmail;

public class ChangeEmailCommandValidator : AbstractValidator<ChangeEmailCommand>
{
    public ChangeEmailCommandValidator()
    {
        RuleFor(dto => dto.CurrentEmail)
            .Matches(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$")
            .WithMessage("Enter a valid email address.");

        RuleFor(dto => dto.NewEmail)
            .Matches(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$")
            .WithMessage("Enter a valid email address.");

        RuleFor(dto => dto.ConfirmNewEmail)
            .Must((dto, confirmNewEmail) => confirmNewEmail == dto.NewEmail)
                .WithMessage("new email and new email confirm fields must match");
    }
}
