using AthleteHub.Domain.Interfaces.Repositories;
using FluentValidation;
using Resturants.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthleteHub.Application.Athletes.Commands.CreateMeasurement
{
    public class CreateMeasurementCommandValidator : AbstractValidator<CreateMeasurementCommand>
    {
        public CreateMeasurementCommandValidator()
        {
            RuleFor(c => c.WeightInKg)
                .GreaterThanOrEqualTo(50).WithMessage("Weight must be at least 50 kg");

            RuleFor(c => c.BodyFatPercentage)
                .GreaterThanOrEqualTo(13).WithMessage("Body fat percentage must be at least 13%");

            RuleFor(c => c.BMI)
                .GreaterThanOrEqualTo(10).WithMessage("BMI must be at least 10");

            RuleFor(c => c.BenchPressWeight)
                .GreaterThanOrEqualTo(0).WithMessage("Bench press weight must be a non-negative number");

            RuleFor(c => c.SquatWeight)
                .GreaterThanOrEqualTo(0).WithMessage("Squat weight must be a non-negative number");

            RuleFor(c => c.DeadliftWeight)
                .GreaterThanOrEqualTo(0).WithMessage("Deadlift weight must be a non-negative number");
        }

    }
}
