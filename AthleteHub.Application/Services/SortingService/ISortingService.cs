using AthleteHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AthleteHub.Application.Services.SortingService
{
    public interface ISortingService
    {
        Expression<Func<Subscribtion, object>> GetSubscribtionSortingExpression(string sortBy);
        Expression<Func<SubscribtionFeature, object>> GetSubscribtionFeatureSortingExpression(string sortBy);
    }
}
