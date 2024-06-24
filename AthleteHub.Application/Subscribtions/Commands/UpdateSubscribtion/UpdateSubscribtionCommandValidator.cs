using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthleteHub.Application.Subscribtions.Commands.UpdateSubscribtion
{
    public class UpdateSubscribtionCommandValidator : AbstractValidator<UpdateSubscribtionCommand>
    {
        public UpdateSubscribtionCommandValidator()
        {
            RuleFor(dto => dto.Name)
                .NotEmpty().WithMessage("Must provide a name.")
                .Length(3, 100).WithMessage("Name length must be between 3 and 100 letters.");

            RuleFor(dto => dto.SubscribtionFeatures)
                .NotEmpty().WithMessage("Subscribtion must have features.");

            RuleFor(dto => dto.price)
                .GreaterThan(0).WithMessage("Subscribtion must have a price greater than 0.");

            RuleFor(dto => dto.DurationInMonths)
                .GreaterThan(0).WithMessage("Subscribtion must have a period greater than 0 months.");
        }
    }
}
