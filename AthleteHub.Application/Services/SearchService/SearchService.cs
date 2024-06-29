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
                        || c.ApplicationUser.LastName.Contains(SearchCritrea)
                        || c.ApplicationUser.UserName.Contains(SearchCritrea)
                        || c.ApplicationUser.Email.Contains(SearchCritrea) && !(c.CoachesBlockedAthletees.Any(cba => cba.AthleteId == athleteId)
                                                && c.IsApproved==true&&c.IsSuspended==false);
        }
        public Expression<Func<Athlete, bool>> GetAthleteSearchExpression(string? SearchCritrea, int subscriptionId)
        {
            if (SearchCritrea == null && subscriptionId==0) return null;

            if (SearchCritrea == null)
                return a => a.AthletesActiveSubscribtions.Any(aas => aas.SubscribtionId == subscriptionId);

            SearchCritrea = SearchCritrea.Trim().ToLower();

            return a => (a.ApplicationUser.FirstName.Contains(SearchCritrea)
                        || a.ApplicationUser.LastName.Contains(SearchCritrea)
                        || a.ApplicationUser.UserName.Contains(SearchCritrea)
                        || a.ApplicationUser.Email.Contains(SearchCritrea) && a.AthletesActiveSubscribtions.Any(aas => aas.SubscribtionId == subscriptionId));
        }

    }
}

