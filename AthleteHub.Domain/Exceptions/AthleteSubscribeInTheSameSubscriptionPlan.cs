using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthleteHub.Domain.Exceptions
{
    public class AthleteSubscribeInTheSameSubscriptionPlan:Exception
    {
        public AthleteSubscribeInTheSameSubscriptionPlan() : base("This Athlete is subscribed with the subscribtion plan") { }
        
    }
}
