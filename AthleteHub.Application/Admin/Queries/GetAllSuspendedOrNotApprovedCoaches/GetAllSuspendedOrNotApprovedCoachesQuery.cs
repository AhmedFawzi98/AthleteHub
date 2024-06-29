using AthleteHub.Application.Coaches.Dtoes;
using MediatR;
using Resturants.Application.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthleteHub.Application.Admin.Queries.GetAllSuspendedOrNotApprovedCoaches
{
    public class GetAllSuspendedOrNotApprovedCoachesQuery : IRequest<PageResultsDto<CoachDto>>
    {
        public int PageSize { get; init; }
        public int PageNumber { get; init; }
        public bool SuspendedCoaches {  get; init; }

        public bool NotApprovedCoaches { get; init; }
    }
}
