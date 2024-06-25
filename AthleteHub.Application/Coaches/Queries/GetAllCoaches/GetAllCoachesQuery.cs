using AthleteHub.Application.Coaches.Dtoes;
using AthleteHub.Domain.Enums;
using MediatR;
using Resturants.Application.Common;
using Resturants.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthleteHub.Application.Coaches.Queries.GetAllCoaches
{
    public class GetAllCoachesQuery: IRequest<PageResultsDto<CoachDto>>
    {
        public bool IncludeCoachesRatings { get; init; }
        public string? SearchCritrea { get; init; }
        public Gender? GenderFilterCritrea { get; init; }
        public RateFilter? RateFilterCritrea { get; init; }
        public AgeFilter? AgeFilterCritrea { get; init; }
        public PriceFilter? PriceFilterCritrea { get; init; }
        public int PageSize { get; init; }
        public int PageNumber { get; init; }
        public SortBy? SortByCritrea { get; init; } = SortBy.rate;
        public SortingDirection SortingDirection { get; init; } = SortingDirection.Ascending;
    }
}
