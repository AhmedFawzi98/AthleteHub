using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthleteHub.Domain.Exceptions
{
    public class SubscribedWithSameCoachException:Exception
    {
        public SubscribedWithSameCoachException() : base("This athlete is Currently subsribed with the same coach") { }
    }
}
