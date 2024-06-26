using AthleteHub.Application.Athletes.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthleteHub.Application.Athletes.Commands.CreateMeasurement
{
    public class CreateMeasurementCommand : IRequest<MeasurementDto>
    {
        public int AthleteId { get; private set; }
        public DateOnly Date { get; init; }
        public decimal WeightInKg { get; init; }
        public decimal? BodyFatPercentage { get; init; }
        public decimal? BMI { get; init; }
        public int? BenchPressWeight { get; init; }
        public int? SquatWeight { get; init; }
        public int? DeadliftWeight { get; init; }
        public void SetAthleteId(int id)
        {
            AthleteId = id;
        }
    }
}
