using FluentValidation;
using MediatR;

namespace AthleteHub.Application.Users.ResetPassword.ConfirmResetPassword;

public class ConfirmResetPasswordCommandValidator : AbstractValidator<ConfirmResetPasswordCommand>
{
    public ConfirmResetPasswordCommandValidator()
    {
        RuleFor(dto => dto.NewPassword)
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long")
            .Matches(@"^(?=.*[a-zA-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]+$")
            .WithMessage("new Password must contain at least one letter, one number, and one special character");

        RuleFor(dto => dto.ConfirmNewPassword)
            .Must((dto, confirmPassword) => confirmPassword == dto.NewPassword)
            .WithMessage("new password and new Password confirm fields must match");
    }
}
