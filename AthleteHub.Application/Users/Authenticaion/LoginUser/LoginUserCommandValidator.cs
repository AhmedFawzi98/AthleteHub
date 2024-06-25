using FluentValidation;

namespace AthleteHub.Application.Users.Authenticaion.LoginUser;

public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
{
    public LoginUserCommandValidator()
    {
        RuleFor(dto => dto.Password)
           .MinimumLength(8).WithMessage("Password must be at least 8 characters long")
           .Matches(@"^(?=.*[a-zA-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]+$")
           .WithMessage("Enter a valid Password");
    }
}
