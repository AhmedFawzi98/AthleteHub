using AthleteHub.Domain.Entities;
using System.Linq.Expressions;


namespace AthleteHub.Application.Services.SearchService
{
    public class SearchService : ISearchService
    {
        public Expression<Func<Coach, bool>> GetCoachSearchExpression(string? SearchCritrea, int? athleteId)
        {
            if (SearchCritrea == null && athleteId == null) return null;

            if (SearchCritrea == null)
                return c => !(c.CoachesBlockedAthletees.Any(cba => cba.AthleteId == athleteId));


            SearchCritrea = SearchCritrea.Trim().ToLower();
            return c => c.ApplicationUser.FirstName.Contains(SearchCritrea)
                        || c.ApplicationUser.FirstName.Contains(SearchCritrea)
                        || c.ApplicationUser.UserName.Contains(SearchCritrea)
                        || c.ApplicationUser.Email.Contains(SearchCritrea) && !(c.CoachesBlockedAthletees.Any(cba => cba.AthleteId == athleteId)
                                                && c.IsApproved==true&&c.IsSuspended==false);
        }
        public Expression<Func<Athlete, bool>> GetAthleteSearchExpression(string? SearchCritrea)
        {
            if (SearchCritrea == null) return null;

            SearchCritrea = SearchCritrea.Trim().ToLower();
            return c => c.ApplicationUser.FirstName.Contains(SearchCritrea)
                        || c.ApplicationUser.FirstName.Contains(SearchCritrea)
                        || c.ApplicationUser.UserName.Contains(SearchCritrea)
                        || c.ApplicationUser.Email.Contains(SearchCritrea);
        }

    }
}

