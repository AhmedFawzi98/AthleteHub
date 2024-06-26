using AthleteHub.Application.Athletes.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthleteHub.Application.Athletes.Queries.FindMeasurement
{
    public class FindMeasurementQuery : IRequest<MeasurementDto>
    {
        public int AthleteId { get; init; }
        public DateOnly Date { get; init; }
    }
}
