using AthleteHub.Application.Athletes.Dtos;
using AthleteHub.Application.Athletes.Queries.FindMeasurement;
using AthleteHub.Domain.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthleteHub.Application.Admin.Queries
{
    public class GetSiteInfoQueryHandler(IUnitOfWork _unitOfWork) : IRequestHandler<GetSiteInfoQuery, SiteInfoDto>
    {
        
        public async Task<SiteInfoDto> Handle(GetSiteInfoQuery request, CancellationToken cancellationToken)
        {
            var coachCount = await _unitOfWork.Coaches.CountAsync();
            var athleteCount = await _unitOfWork.Athletes.CountAsync();
            var activeSubscriptionCount = await _unitOfWork.AthleteActiveSubscribtions.CountAsync();
            var historySubscriptionCount = await _unitOfWork.AthletesSubscribtionsHistory.CountAsync();


            return new SiteInfoDto
            {
                CoachCount = coachCount,
                AthleteCount = athleteCount,
                ActiveSubscriptionCount = activeSubscriptionCount,
                TotalSubscribtionsCount = historySubscriptionCount + activeSubscriptionCount

            };
        }
    }
}
