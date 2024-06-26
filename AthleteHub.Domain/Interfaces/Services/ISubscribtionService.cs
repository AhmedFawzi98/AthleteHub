using AthleteHub.Domain.Entities;

namespace AthleteHub.Domain.Interfaces.Services;

public interface ISubscribtionService
{
    Task CheckSubscriptionExpirations();
    Task EndAthleteActiveSubscription(AthleteActiveSubscribtion athleteActiveSubscribtion);
}
