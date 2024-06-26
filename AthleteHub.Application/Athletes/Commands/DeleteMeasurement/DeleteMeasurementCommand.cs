using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthleteHub.Application.Athletes.Commands.DeleteMeasurement
{
    public class DeleteMeasurementCommand : IRequest
    {
        public DateOnly Date { get; init; }
    }
}
