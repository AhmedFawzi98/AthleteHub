using AthleteHub.Domain.Entities;
using AthleteHub.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AthleteHub.Application.Services.FilterService
{
    public interface IFilterService
    {
        IEnumerable<Expression<Func<Coach, bool>>> GetCoachFilterExpressions(Gender? gender,RateFilter? rateFilter
                                              ,AgeFilter? ageFilter, PriceFilter? priceFilter);
    }
}
