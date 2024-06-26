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
        private readonly IMemoryCache _memoryCache;
        private readonly IUnitOfWork _unitOfWork;


        public PaymentService(IConfiguration configuration, IMemoryCache memoryCache, IUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            StripeConfiguration.ApiKey = _configuration["StripeSittings:Secretkey"];
            _memoryCache = memoryCache;
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
            Session session = await service.CreateAsync(options);

            _memoryCache.Set("SessionId", session.Id);
            _memoryCache.Set("AthleteId", athleteId);
            _memoryCache.Set("SubscriptionId", subscriptionId);

            return session.Url;
        }

        public async Task<string> Success(string sessionId)
        {
            var session = await new SessionService().GetAsync(sessionId);
            var athleteId = session.Metadata["AthleteId"];
            var subscriptionId = session.Metadata["SubscriptionId"];

            var existingAthleteSubscribtion = await _unitOfWork.AthleteActiveSubscribtions.FindAsync(m => m.AthleteId == int.Parse(athleteId) && m.SubscribtionId == int.Parse(subscriptionId));
            var paymentIntentId = session.PaymentIntentId;
            if (existingAthleteSubscribtion is null) 
            { 
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
            }
            else
            {
                throw new InvalidOperationException("you cannot subscribe to the same bundle twice!!");
            }

            return paymentIntentId;
        }


        public Task ProcessWebhookAsync(string json, string stripeSignature)
        {
            throw new NotImplementedException();
        }
    }
}
