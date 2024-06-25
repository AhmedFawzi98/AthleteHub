using AthleteHub.Domain.Enums;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace AthleteHub.Application.Users.Authenticaion.RegisterUser;

public class UpdateUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(dto => dto.Email)
            .Matches(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$")
            .WithMessage("Enter a valid email address.");

        RuleFor(dto => dto.ConfirmEmail)
            .Must((dto, confirmEmail) => confirmEmail == dto.Email)
                .WithMessage("Email and Confirm Email must match");

        RuleFor(dto => dto.UserName)
            .Length(3, 100).WithMessage("name length must be between 3 chars and 100 chars");

        RuleFor(dto => dto.Password)
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long")
            .Matches(@"^(?=.*[a-zA-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]+$")
                .WithMessage("Password must contain at least one letter, one number, and one special character");

        RuleFor(dto => dto.ConfirmPassword)
           .Must((dto, confirmPassword) => confirmPassword == dto.Password)
           .WithMessage("Password and Confirm Password must match");

        RuleFor(dto => dto.FirstName)
            .Length(3, 100).WithMessage("first name length must be between 3 chars and 100 chars");

        RuleFor(dto => dto.LastName)
            .Length(3, 100).WithMessage("last name length must be between 3 chars and 100 chars");

        RuleFor(dto => dto.PhoneNumber)
            .Matches(@"^(010|011|012)\d{8}$")
            .WithMessage("Enter a valid phone number (e.g., 010XXXXXXXX, 011XXXXXXXX, or 012XXXXXXXX).");

        RuleFor(dto => dto.DateOfBirth)
          .Must((dto, dateOfBirth) => DateOnly.FromDateTime(DateTime.Now).Year - dateOfBirth.Year >= 18)
          .WithMessage("age must be above 18");


        RuleFor(dto => dto.Height)
                 .Must((dto, height) => height != null && height >= 150 && height <= 250).WithMessage("valid Height(in cm) is required minmum height = 150cm, maximum height = 250cm.")
                 .When(dto => !dto.IsCoach);

        RuleFor(dto => dto.Bio)
            .MaximumLength(450).WithMessage("bio maximum length is 450 characters")
            .When(dto => dto.Bio != null);

        RuleFor(dto => dto.Certificate)
                 .Must(BeValidPdf).WithMessage("Certificate is requiered and must be a valid PDF file.")
                 .When(dto => dto.IsCoach);
    }
    private bool BeValidPdf(IFormFile certificate)
    {
        return certificate != null && certificate.ContentType.ToLower() == "application/pdf";
    }
}
