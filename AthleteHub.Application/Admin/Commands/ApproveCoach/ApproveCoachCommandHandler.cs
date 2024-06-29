using AthleteHub.Application.Admin.Dtos;
using AthleteHub.Application.Users;
using AthleteHub.Domain.Constants;
using AthleteHub.Domain.Entities;
using AthleteHub.Domain.Exceptions;
using AthleteHub.Domain.Interfaces.Repositories;
using MediatR;

namespace AthleteHub.Application.Admin.Commands.ApproveCoach
{
    public class ApproveCoachCommandHandler(IUnitOfWork _unitOfWork,IUserContext _userContext) : IRequestHandler<ApproveCoachCommand, ApprovalResponseDto>
    {
        public async Task<ApprovalResponseDto> Handle(ApproveCoachCommand request, CancellationToken cancellationToken)
        {
            var currentUser = _userContext.GetCurrentUser();
            if (!currentUser.IsInRole(RolesConstants.Admin))
                throw new UnAuthorizedException();

            var coachToBeApproved = await _unitOfWork.Coaches.FindAsync(c => c.Id == request.CoachId, new() { { c => c.ApplicationUser, new(null, null) } });

            if (coachToBeApproved == null) 
                throw new NotFoundException(nameof(Coach),request.CoachId.ToString());

            coachToBeApproved.IsApproved = true;
            _unitOfWork.Coaches.Update(coachToBeApproved);
            await _unitOfWork.CommitAsync();
            return  new ApprovalResponseDto() { CoachEmail = coachToBeApproved?.ApplicationUser.Email ?? null };
        }
    }
}
