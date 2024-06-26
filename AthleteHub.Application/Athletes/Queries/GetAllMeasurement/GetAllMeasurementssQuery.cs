using AthleteHub.Application.Athletes.Dtos;
using MediatR;
using Resturants.Application.Common;
using Resturants.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthleteHub.Application.Athletes.Queries.GetAllMeasurement
{
    public class GetAllMeasurementssQuery : IRequest<PageResultsDto<MeasurementDto>>
    {
        public int AthleteId { get; init; }
        public int PageSize { get; init; }
        public int PageNumber { get; init; }
        public string? SortBy { get; init; }
        public SortingDirection SortingDirection { get; init; } = SortingDirection.Ascending;
    }
}
