using AthleteHub.Domain.Entities;
using AthleteHub.Domain.Enums;
using Resturants.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AthleteHub.Application.Services.SortingService
{
    public class SortingService : ISortingService
    {
        public Expression<Func<Coach, object>> GetCoachSortingExpression(SortBy? SortByCritrea, SortingDirection sortingDirection)
        {
            if (SortByCritrea == null) return null;
            if (SortByCritrea == SortBy.rate) return c => c.OverallRating;
            if (sortingDirection == SortingDirection.Ascending)
                return c => c.Subscribtions.Min(s => s.price);
            return c => c.Subscribtions.Max(s => s.price);
        }
    }
}
