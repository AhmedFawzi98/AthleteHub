using AthleteHub.Application.Athletes.Dtos;
using AthleteHub.Domain.Enums;
using MediatR;
using Resturants.Application.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthleteHub.Application.Athletes.Queries.GetAthletesBySubscriptionId
{
    public class GetAthletesBySubscriptionIdQuery:IRequest<PageResultsDto<AthleteDto>>
    {
        public int SubscriptionId { get; init; }
        public int PageSize { get; init; }
        public int PageNumber { get; init; }
        public string? SearchCritrea { get; init; }
        public Gender? GenderFilterCritrea { get; init; }
        public AgeFilter? AgeFilterCritrea { get; init; }
    }
}
