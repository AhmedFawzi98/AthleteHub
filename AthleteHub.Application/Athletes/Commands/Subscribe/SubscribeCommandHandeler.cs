using AthleteHub.Application.Athletes.Dtos;
using AthleteHub.Application.Users;
using AthleteHub.Domain.Constants;
using AthleteHub.Domain.Entities;
using AthleteHub.Domain.Exceptions;
using AthleteHub.Domain.Interfaces.Repositories;
using AutoMapper;
using MediatR;
using Stripe.Checkout;

namespace AthleteHub.Application.Athletes.Commands.Subscribe
{
    public class SubscribeCommandHandeler(IUnitOfWork _unitOfWork, IMapper _mapper, IUserContext _userContext) : IRequestHandler<SubscribeCommand, SubscribeResponseDto>
    {
        public async Task<SubscribeResponseDto> Handle(SubscribeCommand request, CancellationToken cancellationToken)
        {
            var currentUser = _userContext.GetCurrentUser();
            if (currentUser==null || !currentUser.IsInRole(RolesConstants.Athlete))
                throw new UnAuthorizedException();

            var athlete = await _unitOfWork.Athletes.FindAsync(a => a.ApplicationUserId == currentUser.Id);

            await AddAthleteCoachAsync(athlete.Id, request.CoachId);
            await AddAthleteActiveSubscribtionAsync(athlete.Id, request.SubscribtionId, request.SubscribtionDurationInMonth,request.SessionId);
            
            await _unitOfWork.CommitAsync();

            return new SubscribeResponseDto() { AthleteEmail=currentUser.Email};
        }
        private async Task AddAthleteCoachAsync(int athleteId,int coachId)
        {
            var athleteCoach = await _unitOfWork.AthletesCoaches.FindAsync(ac => ac.CoachId == coachId && ac.AthleteId == athleteId);

            if (athleteCoach != null) 
            {
                athleteCoach.IsCurrentlySubscribed = true;
            }
            else
            {
                athleteCoach =new AthleteCoach() { 
                 AthleteId = athleteId,
                 CoachId = coachId,
                 IsCurrentlySubscribed = true,
                };
               await _unitOfWork.AthletesCoaches.AddAsync(athleteCoach);
            }
        }

        private async Task AddAthleteActiveSubscribtionAsync(int athleteId,int subscribtionId,int SubscribtionDurationInMonth,string sessionId)
        {
            var session = await new SessionService().GetAsync(sessionId);
            var athleteActiveSubscribtion = new AthleteActiveSubscribtion()
            {
                AthleteId = athleteId,
                SubscribtionId = subscribtionId,
                SubscribtionEndDate = DateOnly.FromDateTime(DateTime.Now).AddMonths(SubscribtionDurationInMonth),
                PaymentIntentId = session.PaymentIntentId
            };
           
            await _unitOfWork.AthleteActiveSubscribtions.AddAsync(athleteActiveSubscribtion);
        }
    }
}
