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
            //check if the athlete is currently subsribed with the same coach
            var athleteCoach = await _unitOfWork.AthletesCoaches.FindAsync(ac => ac.CoachId == request.CoachId && ac.AthleteId == request.AthleteId);
            if (athleteCoach != null && athleteCoach.IsCurrentlySubscribed)
                throw new SubscribedWithSameCoachException();
            //check if the athlete is currently subscribed with the same subcriptoin plan
            var athleteActiveSubscribtion = await _unitOfWork.AthleteActiveSubscribtions
                       .FindAsync(a => a.AthleteId == request.AthleteId && a.SubscribtionId == request.SubscribtionId);
            if (athleteActiveSubscribtion != null)
                throw new AthleteSubscribeInTheSameSubscriptionPlan();

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
            return athleteActiveSubscribtionDto;
        }
    }
}
