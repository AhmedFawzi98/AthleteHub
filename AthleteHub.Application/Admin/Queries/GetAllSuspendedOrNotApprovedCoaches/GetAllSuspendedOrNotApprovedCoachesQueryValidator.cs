﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthleteHub.Application.Admin.Queries.GetAllSuspendedOrNotApprovedCoaches
{
    public class GetAllSuspendedOrNotApprovedCoachesQueryValidator:AbstractValidator<GetAllSuspendedOrNotApprovedCoachesQuery>
    {
        private readonly int[] allowedPageSizes = [5, 10, 15, 20, 50];
        public GetAllSuspendedOrNotApprovedCoachesQueryValidator()
        {
            RuleFor(r => r.PageNumber).GreaterThanOrEqualTo(1)
                .WithMessage("Page number must be equal or greater than 1");


            RuleFor(r => r.PageSize).Custom((value, context) =>
            {
                if (!allowedPageSizes.Contains(value))
                    context.AddFailure("PageSize", $"page size must be either [{string.Join(",", allowedPageSizes)}]");
            });
        }
    }
}
