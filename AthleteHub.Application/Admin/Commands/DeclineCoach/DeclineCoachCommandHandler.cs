
using AthleteHub.Application.Admin.Dtos;
using AthleteHub.Application.Services.BlobStorageService;
using AthleteHub.Application.Users;
using AthleteHub.Domain.Constants;
using AthleteHub.Domain.Entities;
using AthleteHub.Domain.Exceptions;
using AthleteHub.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AthleteHub.Application.Admin.Commands.DeclineCoach
{
    public class DeclineCoachCommandHandler(IUnitOfWork _unitOfWork, IUserContext _userContext, UserManager<ApplicationUser> _userManager, IBlobStorageService _blobStorageService) : IRequestHandler<DeclineCoachCommand, ApprovalResponseDto>
    {
        public async Task<ApprovalResponseDto> Handle(DeclineCoachCommand request, CancellationToken cancellationToken)
        {
            var currentUser = _userContext.GetCurrentUser();
            if (!currentUser.IsInRole(RolesConstants.Admin))
                throw new UnAuthorizedException();

            var coachToBeDeclined = await _unitOfWork.Coaches.FindAsync(c => c.Id == request.CoachId, new() { { c => c.ApplicationUser, new(null, null) } });

            if (coachToBeDeclined == null)
                throw new NotFoundException(nameof(Coach), request.CoachId.ToString());

            if (!string.IsNullOrEmpty(coachToBeDeclined.ApplicationUser.ProfilePicture))
            {
                await _blobStorageService.DeleteBlobAsync(coachToBeDeclined.ApplicationUser.ProfilePicture);
            }
            if (!string.IsNullOrEmpty(coachToBeDeclined.Certificate))
            {
                await _blobStorageService.DeleteBlobAsync(coachToBeDeclined.Certificate);
            }
            
            var deletedUser = await _userManager.FindByIdAsync(coachToBeDeclined.ApplicationUserId);
            await _userManager.DeleteAsync(deletedUser);
            await _unitOfWork.CommitAsync();
            return new ApprovalResponseDto() {CoachEmail= coachToBeDeclined?.ApplicationUser.Email ?? null };
        }
    }
}
