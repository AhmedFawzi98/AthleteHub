
using AthleteHub.Domain.Enums;
using MediatR;

namespace AthleteHub.Application.Athletes.Commands.CalCalculatecalory
{
    public class CalCalculatecaloryCommand:IRequest<decimal>
    {
        public decimal Weight {  get; init; }
        public decimal Height {  get; init; }
        public int  Age{  get; init; }
        public Gender Gender {  get; init; }
        public DailyActivityRate DailyActivityRate { get; init; }
    }
}
