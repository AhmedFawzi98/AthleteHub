using AthleteHub.Application.Athletes.Dtos;
using AthleteHub.Application.Users;
using AthleteHub.Domain.Constants;
using AthleteHub.Domain.Entities;
using AthleteHub.Domain.Exceptions;
using AthleteHub.Domain.Interfaces.Repositories;
using MediatR;

namespace AthleteHub.Application.Athletes.Commands.RateCoach
{
    public class RateCoachCommandHandler(IUnitOfWork _unitOfWork, IUserContext _userContext) : IRequestHandler<RateCoachCommand, RateCoachResponseDto>
    {
        public async Task<RateCoachResponseDto> Handle(RateCoachCommand request, CancellationToken cancellationToken)
        {
            var currentUser = _userContext.GetCurrentUser();
            if (currentUser==null||!currentUser.IsInRole(RolesConstants.Athlete))
                throw new UnAuthorizedException();

            var athlete = await _unitOfWork.Athletes.FindAsync(a => a.ApplicationUserId == currentUser.Id);

            var rateCoachResponseDto = new RateCoachResponseDto();

            var athleteCoach = await _unitOfWork.AthletesCoaches.FindAsync(ac => ac.CoachId == request.CoachId 
                                                     && ac.AthleteId == athlete.Id);

            if (athleteCoach==null)
            {
                rateCoachResponseDto.Ratesucceded = false;
                return rateCoachResponseDto;
            }

            var coach = await _unitOfWork.Coaches.FindAsync(c => c.Id == request.CoachId, new() { { c => c.CoachesRatings, new(null, null) } });

            if (coach == null)
                throw new NotFoundException(nameof(Coach), request.CoachId.ToString());

            var coachRating = coach.CoachesRatings.FirstOrDefault(cr => cr.CoachId == request.CoachId && cr.AthleteId == athlete.Id);
            if (coachRating != null)
            {
                coachRating.Rate = request.Rate;
                coachRating.Comment = request.Comment;
            }
            else
                AddNewRating(coach, athlete.Id, request.Rate, request.Comment);

            coach.OverallRating = (coach.OverallRating + request.Rate) / coach.RatingsCount;
            _unitOfWork.Coaches.Update(coach);
            await _unitOfWork.CommitAsync();

            rateCoachResponseDto.Ratesucceded = true;
            return rateCoachResponseDto;
            
        }
        private void AddNewRating(Coach coach, int athleteId, int rate, string comment)
        {
            var coachRating = new CoachRating()
            {   
                CoachId = coach.Id, 
                AthleteId = athleteId,
                Rate = rate, 
                Comment = comment 
            };
            coach.CoachesRatings.Add(coachRating);
            coach.RatingsCount++;
        }
    }
}
