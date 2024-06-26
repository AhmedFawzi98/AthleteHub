using AthleteHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AthleteHub.Application.Services.SearchService
{
    public interface ISearchService
    {
        Expression<Func<Coach, bool>> GetCoachSearchExpression(string? SearchCritrea, int? athleteId);
    }
}
