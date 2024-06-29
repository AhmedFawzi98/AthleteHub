using AthleteHub.Domain.Entities;
using AthleteHub.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Stripe;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace AthleteHub.Infrastructure.Authorization.Services.Payment
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;


        public PaymentService(IConfiguration configuration, IUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            StripeConfiguration.ApiKey = _configuration["StripeSittings:Secretkey"];
            _unitOfWork = unitOfWork;
        }
        public async Task<string> CreateCheckoutSessionAsync(decimal price, string subscriptionName, int athleteId, int subscriptionId)
        {
            var successUrl = "http://localhost:5068/api/Payment/Success?sessionId={CHECKOUT_SESSION_ID}";

            var cancelUrl = "http://yourtestserver/cancel";

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions>
            {
                new SessionLineItemOptions
                {
                     PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = "usd",
                        UnitAmount = Convert.ToInt32(price) * 100, // Convert to cents for Stripe
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = subscriptionName
                        },
                    },
                    Quantity = 1,
                },
            },
                Mode = "payment",
                SuccessUrl = successUrl,
                CancelUrl = cancelUrl,
                Metadata = new Dictionary<string, string>
            {
                { "AthleteId", athleteId.ToString() },
                { "SubscriptionId", subscriptionId.ToString() }
            }
            };
            var service = new SessionService();
            var existingSubscription = _unitOfWork.AthleteActiveSubscribtions.FindAsync(acs => acs.AthleteId == athleteId && acs.SubscribtionId == subscriptionId);
            if (existingSubscription is not null)
            {
                throw new InvalidOperationException("you cannot subscribe to the same bundle twice!!");
            }
            Session session = await service.CreateAsync(options);

       

            return session.Url;
        }

        public async Task<string> Success(string sessionId)
        {
            var session = await new SessionService().GetAsync(sessionId);
            var athleteId = session.Metadata["AthleteId"];
            var subscriptionId = session.Metadata["SubscriptionId"];

            var paymentIntentId = session.PaymentIntentId;

            var newAthleteSubscription = new AthleteActiveSubscribtion()
            {
                AthleteId = int.Parse(athleteId),
                SubscribtionId = int.Parse(subscriptionId),
                PaymentIntentId = paymentIntentId,
                SubscribtionStartDate = DateOnly.FromDateTime(DateTime.Now),
                SubscribtionEndDate = DateOnly.FromDateTime(DateTime.Now.AddDays(90))
            };
            await _unitOfWork.AthleteActiveSubscribtions.AddAsync(newAthleteSubscription);
            await _unitOfWork.CommitAsync();
            return paymentIntentId;
        }
    }
}
