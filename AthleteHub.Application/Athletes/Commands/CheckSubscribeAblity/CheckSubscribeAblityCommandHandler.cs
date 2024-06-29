using AthleteHub.Application.Athletes.Commands.Subscribe;
using AthleteHub.Application.Athletes.Dtos;
using AthleteHub.Application.Users;
using AthleteHub.Domain.Constants;
using AthleteHub.Domain.Exceptions;
using AthleteHub.Domain.Interfaces.Repositories;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthleteHub.Application.Athletes.Commands.CheckSubscribeAblity
{
    public class CheckSubscribeAblityCommandHandler(IUnitOfWork _unitOfWork, IMapper _mapper, IUserContext _userContext) : IRequestHandler<CheckSubscribeAblityCommand, CheckSubscribeResponseDto>
    {
        public async Task<CheckSubscribeResponseDto> Handle(CheckSubscribeAblityCommand request, CancellationToken cancellationToken)
        {
            var currentUser = _userContext.GetCurrentUser();
            if (!currentUser.IsInRole(RolesConstants.Athlete))
                throw new UnAuthorizedException();

            var athlete = await _unitOfWork.Athletes.FindAsync(a => a.ApplicationUserId == currentUser.Id);

            var athleteCoach = await _unitOfWork.AthletesCoaches.FindAsync(ac => ac.CoachId == request.CoachId && ac.AthleteId == athlete.Id, new() { { ac => ac.Coach, new(null, null) } });

            var checkSubscribeResponseDto = new CheckSubscribeResponseDto();
            if (athleteCoach != null && (athleteCoach.IsCurrentlySubscribed || athleteCoach.Coach.IsSuspended == true))
            {
                checkSubscribeResponseDto.CanSubscribe = false;
                return checkSubscribeResponseDto;
            }

            checkSubscribeResponseDto.CanSubscribe=true;
            return checkSubscribeResponseDto;
        }
    }
}
