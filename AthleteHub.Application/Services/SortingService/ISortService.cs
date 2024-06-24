using AthleteHub.Domain.Entities;
using AthleteHub.Domain.Enums;
using Resturants.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AthleteHub.Application.Services.SortingService
{
    public interface ISortService
    {
        Expression<Func<Coach, object>> GetCoachSortingExpression(SortBy? sortBy,SortingDirection sortingDirection);
    }
}
