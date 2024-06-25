using AthleteHub.Application.Subscribtions.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthleteHub.Application.Subscribtions.Queries.GetAllSubscribtionsByCoachId
{
    public class GetAllSubscribtionsQueryByCoachIdValidator : AbstractValidator<GetAllSubscribtionsQueryByCoachId>
    {
        private readonly int[] allowedPageSizes = [5, 10, 15, 20, 50];

        private readonly string[] allowedSortingByProperties =
            [
                nameof(SubscribtionDto.Name),
                nameof(SubscribtionDto.price),
            ];

        public GetAllSubscribtionsQueryByCoachIdValidator()
        {
            RuleFor(r => r.PageNumber).GreaterThanOrEqualTo(1).WithMessage("Page number must be equal or greater than 1.");

            RuleFor(r => r.PageSize).Custom((value,context)=> {
                if (!allowedPageSizes.Contains(value))
                    context.AddFailure("PageSize", $"Page size must be either [{string.Join(",", allowedPageSizes)}]");
            });

            RuleFor(r => r.SortBy).Custom((value, context) =>
            {
                if (!allowedSortingByProperties.Contains(value))
                    context.AddFailure($"Sorting is optional, but if sorting is requested, it must be by one of these properties [{string.Join(",", allowedSortingByProperties)}]");
            }).When(r => r.SortBy != null);
        }
    }
}
