using AthleteHub.Application.Athletes.Commands.AddToFavourite;
using AthleteHub.Application.Athletes.Dtos;
using AthleteHub.Domain.Enums;
using MediatR;

namespace AthleteHub.Application.Athletes.Commands.CalCalculatecalory
{
    public class CalCalculatecaloryCommandHandeler : IRequestHandler<CalCalculatecaloryCommand, decimal>
    {
        public Task<decimal> Handle(CalCalculatecaloryCommand request, CancellationToken cancellationToken)
        {
            decimal bmr;
            if (request.Gender == Gender.Male)
            {
                bmr = 10 * request.Weight + 6.25m * request.Height - 5 * request.Age + 5;
            }
            else 
            {
                bmr = 10 * request.Weight + 6.25m * request.Height - 5 * request.Age - 161;
            }

            decimal activityFactor;
            switch (request.DailyActivityRate)
            {
                case DailyActivityRate.Sedentary:
                    activityFactor = 1.2m;
                    break;
                case DailyActivityRate.LightlyActive:
                    activityFactor = 1.375m;
                    break;
                case DailyActivityRate.ModeratelyActive:
                    activityFactor = 1.55m;
                    break;
                case DailyActivityRate.VeryActive:
                    activityFactor = 1.725m;
                    break;
                case DailyActivityRate.SuperActive:
                    activityFactor = 1.9m;
                    break;
                default:
                    activityFactor = 1.2m; 
                    break;
            }

            
            decimal tdee = bmr * activityFactor;

            return Task.FromResult(tdee);
        }
    }
}
