using AthleteHub.Application.Subscribtions.Dtos;
using AthleteHub.Application.Subscribtions.Queries.FindSubscribtion;
using AthleteHub.Domain.Interfaces.Repositories;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthleteHub.Application.Athletes.Queries.GetAthleteSubscribtion
{
    public class GetAthleteSubscribtionQueryHandler(IUnitOfWork _unitOfWork, IMapper _mapper) : IRequestHandler<GetAthleteSubscribtionQuery, List<SubscribtionDto>>
    {
        public async Task<List<SubscribtionDto>> Handle(GetAthleteSubscribtionQuery request, CancellationToken cancellationToken)
        {
            var includes = new List<string>{
               "AthletesActiveSubscribtions"
            };
            if (request.IncludeSubscribtionFeature)
            {
                includes.Add("AthletesActiveSubscribtions.Subscribtion.SubscribtionsFeatures");
                includes.Add("AthletesActiveSubscribtions.Subscribtion.SubscribtionsFeatures.Feature");
            }

            var athlete = await _unitOfWork.Athletes.GetAllAsync(
                a => a.ApplicationUserId == "16780a3e-c879-451b-95d0-2296037c14b9", includes);

            var currentAthlete = athlete.FirstOrDefault();

            var activeSubscribtions = currentAthlete.AthletesActiveSubscribtions.Select(afc => afc.Subscribtion).ToList();

            if (currentAthlete == null)
            {
                return new List<SubscribtionDto>();
            }
            var activeSubscribtionsDto = _mapper.Map<List<SubscribtionDto>>(activeSubscribtions);

            return activeSubscribtionsDto;

        }
    }
}
