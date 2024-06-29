using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthleteHub.Application.Athletes.Dtos
{
    public class SiteInfoDto
    {
        public int CoachCount { get; set; }
        public int AthleteCount { get; set; }
        public int ActiveSubscriptionCount { get; set; }
        public int TotalSubscribtionsCount { get; set; }
    }
}
