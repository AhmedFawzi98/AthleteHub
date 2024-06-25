using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace AthleteHub.Application.Users.UpdateUser;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {

        RuleFor(dto => dto.UserName)
            .Length(3, 100).WithMessage("name length must be between 3 chars and 100 chars")
            .When(dto => dto.UserName != null);


        RuleFor(dto => dto.FirstName)
            .Length(3, 100).WithMessage("first name length must be between 3 chars and 100 chars")
            .When(dto => dto.FirstName != null);


        RuleFor(dto => dto.LastName)
            .Length(3, 100).WithMessage("last name length must be between 3 chars and 100 chars")
            .When(dto => dto.LastName != null);


        RuleFor(dto => dto.PhoneNumber)
            .Matches(@"^(010|011|012)\d{8}$")
            .WithMessage("Enter a valid phone number (e.g., 010XXXXXXXX, 011XXXXXXXX, or 012XXXXXXXX).")
            .When(dto => dto.PhoneNumber != null);


        RuleFor(dto => dto.DateOfBirth)
          .Must((dto, dateOfBirth) => DateOnly.FromDateTime(DateTime.Now).Year - dateOfBirth.Value.Year >= 18)
          .WithMessage("age must be above 18")
            .When(dto => dto.DateOfBirth != null);


        RuleFor(dto => dto.Bio)
            .MaximumLength(450).WithMessage("bio maximum length is 450 characters")
            .When(dto => dto.Bio != null);

    }
}
