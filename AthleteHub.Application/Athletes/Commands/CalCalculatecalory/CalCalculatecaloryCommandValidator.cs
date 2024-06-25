using FluentValidation;

namespace AthleteHub.Application.Athletes.Commands.CalCalculatecalory
{
    public class CalCalculatecaloryCommandValidator: AbstractValidator<CalCalculatecaloryCommand>
    {
        public CalCalculatecaloryCommandValidator()
        {
            RuleFor(x => x.Weight)
            .GreaterThan(0).WithMessage("Weight must be greater than zero.");

            RuleFor(x => x.Height)
                .GreaterThan(0).WithMessage("Height must be greater than zero.");

            RuleFor(x => x.Age)
                .GreaterThan(0).WithMessage("Age must be a positive integer.");

            RuleFor(x => x.Gender)
                .IsInEnum().WithMessage("Gender must be specified and valid.");

            RuleFor(x => x.DailyActivityRate)
                .IsInEnum().WithMessage("Daily activity rate must be specified and valid.");
        }
        
    }
}
