using AthleteHub.Application.Users.ChangePassword;
using AthleteHub.Domain.Enums;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace AthleteHub.Application.Users.RegisterUser;

public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
{
    public ChangePasswordCommandValidator()
    {
        RuleFor(dto => dto.CurrentPassword)
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long")
            .Matches(@"^(?=.*[a-zA-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]+$")
                .WithMessage("Enter a valid Password");

        RuleFor(dto => dto.NewPassword)
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long")
            .Matches(@"^(?=.*[a-zA-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]+$")
                .WithMessage("new Password must contain at least one letter, one number, and one special character");

        RuleFor(dto => dto.ConfirmNewPassword)
           .Must((dto, confirmPassword) => confirmPassword == dto.NewPassword)
           .WithMessage("new password and new Password confirm fields must match");
    }
}
