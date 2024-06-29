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


namespace AthleteHub.Application.Athletes.Queries.GetAllfavoriteCoaches
{
    public class GetAllfavoriteCoachesQuery : IRequest<PageResultsDto< CoachDto>>
    {
        public int AthleteId { get; set; }
        public SortingDirection SortingDirection { get; init; } = SortingDirection.Ascending;
        public int PageSize { get; init; }
        public int PageNumber { get; init; }
    }
}
