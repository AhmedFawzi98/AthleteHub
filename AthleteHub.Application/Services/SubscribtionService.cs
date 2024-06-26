using AthleteHub.Domain.Entities;
using AthleteHub.Domain.Interfaces.Repositories;
using AthleteHub.Domain.Interfaces.Services;

namespace AthleteHub.Application.Services
{
    public class SubscribtionService(IUnitOfWork _unitOfWork) : ISubscribtionService
    {
        public async Task CheckSubscriptionExpirations()
        {
            var today = DateOnly.FromDateTime(DateTime.Now);
            var expiredAthleteActiveSubscribtions = await _unitOfWork.AthleteActiveSubscribtions.GetAllAsync(aas =>
                aas.SubscribtionEndDate!.Value <= today, [nameof(AthleteActiveSubscribtion.Subscribtion)]);

            foreach(var expiredAthleteActiveSubscribtion in expiredAthleteActiveSubscribtions)
            {
                await EndAthleteActiveSubscription(expiredAthleteActiveSubscribtion);
            }

            await _unitOfWork.CommitAsync();
        }

        public async Task EndAthleteActiveSubscription(AthleteActiveSubscribtion athleteActivesubscribtion)
        {
            var athleteCoach = await _unitOfWork.AthletesCoaches.FindAsync(ac =>
                ac.AthleteId == athleteActivesubscribtion.AthleteId &&  ac.CoachId == athleteActivesubscribtion.Subscribtion.CoachId);
            
            athleteCoach.IsCurrentlySubscribed = false;

            await _unitOfWork.AthletesSubscribtionsHistory.AddAsync(new()
            {
                AthleteId = athleteActivesubscribtion.AthleteId,
                SubscribtionId = athleteActivesubscribtion.SubscribtionId,
                SubscribtionStartDate = athleteActivesubscribtion.SubscribtionStartDate,
                SubscribtionEndDate = athleteActivesubscribtion.SubscribtionEndDate,
                PaymentIntentId = athleteActivesubscribtion.PaymentIntentId
            });

            _unitOfWork.AthleteActiveSubscribtions.Delete(athleteActivesubscribtion);
        }
    }
}
