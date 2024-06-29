using AthleteHub.Domain.Entities;
using AthleteHub.Domain.Enums;
using System.Linq.Expressions;

namespace AthleteHub.Application.Services.FilterService
{
    public interface IFilterService
    {
        IEnumerable<Expression<Func<Coach, bool>>> GetCoachFilterExpressions(Gender? gender, RateFilter? rateFilter
                                              , AgeFilter? ageFilter, PriceFilter? priceFilter);
        IEnumerable<Expression<Func<Athlete, bool>>> GetAthleteFilterExpressions(Gender? gender, AgeFilter? ageFilter);
    }
}
