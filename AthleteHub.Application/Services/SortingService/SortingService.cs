using AthleteHub.Domain.Entities;
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
        Dictionary<string, Expression<Func<Subscribtion, object>>> subscribtionExpressionsDictionary = new()
        {
            { nameof(Subscribtion.Name).ToLower(), r => r.Name },
            { nameof(Subscribtion.price), r => r.price }
        };
        Dictionary<string, Expression<Func<SubscribtionFeature, object>>> subscribtionFeatureExpressionDictionary = new()
        {
            { nameof(SubscribtionFeature.Feature.Name).ToLower(), r => r.Feature.Name
            },
            { nameof(SubscribtionFeature.Subscribtion.Name).ToLower(), r => r.Subscribtion.Name
            },
            { nameof(SubscribtionFeature.Subscribtion.price), r => r.Subscribtion.price
            }
        };

        public Expression<Func<Subscribtion, object>> GetSubscribtionSortingExpression(string sortBy)
        {
            return subscribtionExpressionsDictionary[sortBy.ToLower()];
        }

        public Expression<Func<SubscribtionFeature, object>> GetSubscribtionFeatureSortingExpression(string sortBy)
        {
            return subscribtionFeatureExpressionDictionary[sortBy.ToLower()];
        }
    }
}
