using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthleteHub.Application.Athletes.Dtos
{
    public class MeasurementDto
    {
        public int AthleteId { get; set; }
        public DateOnly Date { get; set; }
        public decimal WeightInKg { get; set; }
        public decimal? BodyFatPercentage { get; set; }
        public decimal? BMI { get; set; }
        public int? BenchPressWeight { get; set; }
        public int? SquatWeight { get; set; }
        public int? DeadliftWeight { get; set; }
    }
}
