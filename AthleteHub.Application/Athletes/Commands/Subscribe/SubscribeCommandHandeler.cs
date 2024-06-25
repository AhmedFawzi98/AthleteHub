using AthleteHub.Application.Athletes.Dtos;
using AthleteHub.Domain.Entities;
using AthleteHub.Domain.Exceptions;
using AthleteHub.Domain.Interfaces.Repositories;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthleteHub.Application.Athletes.Commands.Subscribe
{
    public class SubscribeCommandHandeler : IRequestHandler<SubscribeCommand, AthleteActiveSubscribtionDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SubscribeCommandHandeler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<AthleteActiveSubscribtionDto> Handle(SubscribeCommand request, CancellationToken cancellationToken)
        {
           
            var athleteCoach = await _unitOfWork.AthletesCoaches.FindAsync(ac => ac.CoachId == request.CoachId && ac.AthleteId == request.AthleteId);
            //Edit
            if (athleteCoach != null && athleteCoach.IsCurrentlySubscribed)
                return new AthleteActiveSubscribtionDto() { AthleteId = request.AthleteId
                                                   ,SubscribtionId=request.SubscribtionId, CanSubscribe=false};


            var athleteActiveSubscribtion = await _unitOfWork.AthleteActiveSubscribtions
                       .FindAsync(a => a.AthleteId == request.AthleteId && a.SubscribtionId == request.SubscribtionId);
            //Edit
            if (athleteActiveSubscribtion != null)
                return new AthleteActiveSubscribtionDto()
                {
                    AthleteId = request.AthleteId                          ,
                    SubscribtionId = request.SubscribtionId,
                    CanSubscribe = false
                };

            //Do Payment in someway

            if (athleteCoach != null)
            {
                athleteCoach.IsCurrentlySubscribed = true;
                _unitOfWork.AthletesCoaches.Update(athleteCoach);
            }
            else
            {
                athleteCoach = new AthleteCoach();
                athleteCoach.AthleteId = request.AthleteId;
                athleteCoach.CoachId = request.CoachId;
                athleteCoach.IsCurrentlySubscribed = true;
                await _unitOfWork.AthletesCoaches.AddAsync(athleteCoach);
            }
            athleteActiveSubscribtion = new AthleteActiveSubscribtion();
            athleteActiveSubscribtion.AthleteId = request.AthleteId;
            athleteActiveSubscribtion.SubscribtionId = request.SubscribtionId;
            athleteActiveSubscribtion.SubscribtionEndDate = DateOnly.FromDateTime(DateTime.Now).AddMonths(request.SubscribtionDurationInMonth);
            await _unitOfWork.AthleteActiveSubscribtions.AddAsync(athleteActiveSubscribtion);
            await _unitOfWork.CommitAsync();
            var athleteActiveSubscribtionDto = _mapper.Map<AthleteActiveSubscribtionDto>(athleteActiveSubscribtion);
            athleteActiveSubscribtionDto.CanSubscribe = true;
            return athleteActiveSubscribtionDto;
        }
    }
}
