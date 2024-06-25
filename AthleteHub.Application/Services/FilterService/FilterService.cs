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
    public class FilterService : IFilterService
    {
        Dictionary<Gender, Expression<Func<Coach, bool>>> GenderFilterExpressionDictionary = new()
        {
            { Gender.Male, c=>c.ApplicationUser.Gender==Gender.Male },
            { Gender.Female, c=>c.ApplicationUser.Gender==Gender.Male },
        };
        Dictionary<RateFilter, Expression<Func<Coach, bool>>> RateFilterExpressionDictionary = new()
        {
            { RateFilter.moreThanOne, c=>c.OverallRating>1 },
            { RateFilter.moreThanTwo, c=>c.OverallRating>2 },
            { RateFilter.moreThanThree, c=>c.OverallRating>3 },
            { RateFilter.moreThanFour, c=>c.OverallRating>4 },
        };
        Dictionary<PriceFilter, Expression<Func<Coach, bool>>> PriceFilterExpressionDictionary = new()
        {
            { PriceFilter.lessThan500, c=>c.Subscribtions.Any(s=>s.price<500) },
            { PriceFilter.between500and1000, c=>c.Subscribtions.Any(s=>s.price>=500&&s.price<=1000) },
            { PriceFilter.between1000and1500, c=>c.Subscribtions.Any(s=>s.price>=1000&&s.price<=1500) },
            { PriceFilter.moreThan1500, c=>c.Subscribtions.Any(s=>s.price>1500) },
        };
        Dictionary<AgeFilter, Expression<Func<Coach, bool>>> AgeFilterExpressionDictionary = new()
        {
            { AgeFilter.lessThan20, c=> DateTime.Now.Year- c.ApplicationUser.DateOfBirth.Year<=20},

            { AgeFilter.between20and25, c=> DateTime.Now.Year- c.ApplicationUser.DateOfBirth.Year>=20 && 
                                              DateTime.Now.Year- c.ApplicationUser.DateOfBirth.Year<=25},

            { AgeFilter.between25and30, c=> DateTime.Now.Year- c.ApplicationUser.DateOfBirth.Year>=25 &&
                                              DateTime.Now.Year- c.ApplicationUser.DateOfBirth.Year<=30},
            { AgeFilter.moeThan30, c=> DateTime.Now.Year- c.ApplicationUser.DateOfBirth.Year>=30},
        };
        public IEnumerable<Expression<Func<Coach, bool>>> GetCoachFilterExpressions(Gender? genderFilter, RateFilter? rateFilter, AgeFilter? ageFilter, PriceFilter? priceFilter)
        {
            List<Expression<Func<Coach, bool>>> filterExpressions = new();

            bool found= false;
            if (genderFilter != null)
            {
                found = true;
                filterExpressions.Add(GenderFilterExpressionDictionary[(Gender)genderFilter]);
            }
                
            if (rateFilter != null)
            {
                found = true;
                filterExpressions.Add(RateFilterExpressionDictionary[(RateFilter)rateFilter]);
            }
               
            if (ageFilter != null)
            {
                found = true;
                filterExpressions.Add(AgeFilterExpressionDictionary[(AgeFilter)ageFilter]);
            }
                
            if (priceFilter != null)
            {
                found=true;
                filterExpressions.Add(PriceFilterExpressionDictionary[(PriceFilter)priceFilter]);
            }
            return found == false ? null : filterExpressions;
        }
    }
}
