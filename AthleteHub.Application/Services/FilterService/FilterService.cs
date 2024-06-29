using AthleteHub.Domain.Entities;
using AthleteHub.Domain.Enums;
using System.Linq.Expressions;


namespace AthleteHub.Application.Services.FilterService
{
    public class FilterService : IFilterService
    {
        Dictionary<Gender, Expression<Func<Coach, bool>>> CoachGenderFilterExpressionDictionary = new()
        {
            { Gender.Male, c=>c.ApplicationUser.Gender==Gender.Male },
            { Gender.Female, c=>c.ApplicationUser.Gender==Gender.Female },
        };
        Dictionary<RateFilter, Expression<Func<Coach, bool>>> CoachRateFilterExpressionDictionary = new()
        {
            { RateFilter.moreThanOne, c=>c.OverallRating>1 },
            { RateFilter.moreThanTwo, c=>c.OverallRating>2 },
            { RateFilter.moreThanThree, c=>c.OverallRating>3 },
            { RateFilter.moreThanFour, c=>c.OverallRating>4 },
        };
        Dictionary<PriceFilter, Expression<Func<Coach, bool>>> CoachPriceFilterExpressionDictionary = new()
        {
            { PriceFilter.lessThan500, c=>c.Subscribtions.Any(s=>s.price<500) },
            { PriceFilter.between500and1000, c=>c.Subscribtions.Any(s=>s.price>=500&&s.price<=1000) },
            { PriceFilter.between1000and1500, c=>c.Subscribtions.Any(s=>s.price>=1000&&s.price<=1500) },
            { PriceFilter.moreThan1500, c=>c.Subscribtions.Any(s=>s.price>1500) },
        };
        Dictionary<AgeFilter, Expression<Func<Coach, bool>>> CoachAgeFilterExpressionDictionary = new()
        {
            { AgeFilter.lessThan20, c=> DateTime.Now.Year- c.ApplicationUser.DateOfBirth.Year<=20},

            { AgeFilter.between20and25, c=> DateTime.Now.Year- c.ApplicationUser.DateOfBirth.Year>=20 &&
                                              DateTime.Now.Year- c.ApplicationUser.DateOfBirth.Year<=25},

            { AgeFilter.between25and30, c=> DateTime.Now.Year- c.ApplicationUser.DateOfBirth.Year>=25 &&
                                              DateTime.Now.Year- c.ApplicationUser.DateOfBirth.Year<=30},
            { AgeFilter.moeThan30, c=> DateTime.Now.Year- c.ApplicationUser.DateOfBirth.Year>=30},
        };


        Dictionary<Gender, Expression<Func<Athlete, bool>>> AthleteGenderFilterExpressionDictionary = new()
        {
            { Gender.Male, a=>a.ApplicationUser.Gender==Gender.Male },
            { Gender.Female, a=>a.ApplicationUser.Gender==Gender.Female },
        };
        Dictionary<AgeFilter, Expression<Func<Athlete, bool>>> AthleteAgeFilterExpressionDictionary = new()
        {
            { AgeFilter.lessThan20, a=> DateTime.Now.Year- a.ApplicationUser.DateOfBirth.Year<=20},

            { AgeFilter.between20and25, a=> DateTime.Now.Year- a.ApplicationUser.DateOfBirth.Year>=20 &&
                                              DateTime.Now.Year- a.ApplicationUser.DateOfBirth.Year<=25},

            { AgeFilter.between25and30, a=> DateTime.Now.Year- a.ApplicationUser.DateOfBirth.Year>=25 &&
                                              DateTime.Now.Year- a.ApplicationUser.DateOfBirth.Year<=30},
            { AgeFilter.moeThan30, a=> DateTime.Now.Year- a.ApplicationUser.DateOfBirth.Year>=30},
        };

        public IEnumerable<Expression<Func<Athlete, bool>>> GetAthleteFilterExpressions(Gender? gender, AgeFilter? ageFilter)
        {
            List<Expression<Func<Athlete, bool>>> filterExpressions = new();

            bool found = false;
            if (gender != null)
            {
                found = true;
                filterExpressions.Add(AthleteGenderFilterExpressionDictionary[(Gender)gender]);
            }
            if (ageFilter != null)
            {
                found = true;
                filterExpressions.Add(AthleteAgeFilterExpressionDictionary[(AgeFilter)ageFilter]);
            }
            return found == false ? null : filterExpressions;
        }

        public IEnumerable<Expression<Func<Coach, bool>>> GetCoachFilterExpressions(Gender? genderFilter, RateFilter? rateFilter, AgeFilter? ageFilter, PriceFilter? priceFilter)
        {
            List<Expression<Func<Coach, bool>>> filterExpressions = new();

            bool found = false;
            if (genderFilter != null)
            {
                found = true;
                filterExpressions.Add(CoachGenderFilterExpressionDictionary[(Gender)genderFilter]);
            }

            if (rateFilter != null)
            {
                found = true;
                filterExpressions.Add(CoachRateFilterExpressionDictionary[(RateFilter)rateFilter]);
            }

            if (ageFilter != null)
            {
                found = true;
                filterExpressions.Add(CoachAgeFilterExpressionDictionary[(AgeFilter)ageFilter]);
            }

            if (priceFilter != null)
            {
                found = true;
                filterExpressions.Add(CoachPriceFilterExpressionDictionary[(PriceFilter)priceFilter]);
            }
            return found == false ? null : filterExpressions;
        }
    }
}
