using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthleteHub.Infrastructure.Authorization.Services.Payment
{

    public interface IPaymentService
    {
        Task<string> CreateCheckoutSessionAsync(decimal price, string productName, int athleteId, int subscriptionId);
        Task ProcessWebhookAsync(string json, string stripeSignature);
        Task<string> Success(string sessionId);
    }

}
