using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthleteHub.Infrastructure.Authorization.Services.Payment
{
    public class CreateCheckoutSessionRequest
    {
        public decimal Price { get; set; }
        public string SubscriptionName { get; set; }
        public int AthleteId { get; set; }
        public int SubscriptionId { get; set; }
    }
}
