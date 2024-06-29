using AthleteHub.Domain.Entities;
using AthleteHub.Domain.Enums;
using Resturants.Domain.Enums;
using System.Linq.Expressions;


namespace AthleteHub.Application.Services.SortingService
{
    public interface ISortingService
    {
        Expression<Func<Coach, object>> GetCoachSortingExpression(SortBy? sortBy, SortingDirection sortingDirection);
    }
}
