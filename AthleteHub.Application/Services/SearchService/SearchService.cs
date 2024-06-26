using AthleteHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AthleteHub.Application.Services.SearchService
{
    public class SearchService : ISearchService
    {
        public Expression<Func<Coach, bool>> GetCoachSearchExpression(string? SearchCritrea,int? athleteId)
        {
            if (SearchCritrea == null && athleteId == null) return null;

            if(SearchCritrea == null)
                return c => !(c.CoachesBlockedAthletees.Any(cba => cba.AthleteId==athleteId));
            

            SearchCritrea = SearchCritrea.Trim().ToLower();
            return c => c.ApplicationUser.FirstName.Contains(SearchCritrea)
                        || c.ApplicationUser.FirstName.Contains(SearchCritrea)
                        || c.ApplicationUser.UserName.Contains(SearchCritrea)
                        || c.ApplicationUser.Email.Contains(SearchCritrea)&& !(c.CoachesBlockedAthletees.Any(cba => cba.AthleteId == athleteId));
           
        }
    }
}
