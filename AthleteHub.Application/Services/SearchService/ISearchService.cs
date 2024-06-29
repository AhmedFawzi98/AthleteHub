using AthleteHub.Domain.Entities;
using System.Linq.Expressions;


namespace AthleteHub.Application.Services.SearchService
{
    public interface ISearchService
    {
        Expression<Func<Coach, bool>> GetCoachSearchExpression(string? SearchCritrea, int? athleteId);
        Expression<Func<Athlete, bool>> GetAthleteSearchExpression(string? SearchCritrea,int subscriptionId);
    }
}
