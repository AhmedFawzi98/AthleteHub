using AthleteHub.Application.Users.Dtos;
using AthleteHub.Domain.Constants;
using AthleteHub.Domain.Entities;
using AthleteHub.Domain.Exceptions;
using AthleteHub.Domain.Interfaces.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Resturants.Application.Users;
using static AthleteHub.Domain.Exceptions.BadRequestException;

namespace AthleteHub.Application.Users.ActivateAndDeactivateUser;

public class ActivateOrDeactivateUserCommandHandler(IMapper _mapper, UserManager<ApplicationUser> _userManager,
    IUnitOfWork _unitOfWork, IUserContext _userContext) : IRequestHandler<ActivateOrDeactivateUserCommand>
{
    public async Task Handle(ActivateOrDeactivateUserCommand request, CancellationToken cancellationToken)
    {
        var currentUser = _userContext.GetCurrentUser()
            ?? throw new UnAuthorizedException();

        var user = await _userManager.FindByIdAsync(currentUser.Id)
            ?? throw new NotFoundException(nameof(ApplicationUser), currentUser.Id);

        if (await HandleAthleteActivationAsync(request, user, currentUser.Id) ||  await HandleCoachActivationAsync(request, user, currentUser.Id))
        {
            await _userManager.UpdateAsync(user);
        }
    }

    private async Task<bool> HandleAthleteActivationAsync(ActivateOrDeactivateUserCommand request, ApplicationUser user, string currentUserId)
    {
        var athlete = await _unitOfWork.Athletes.FindAsync(c => c.ApplicationUserId == currentUserId, null!);
        if (athlete == null)
            return false;

        user.IsActive = !request.IsDeactivating;
        return true;
    }

    private async Task<bool> HandleCoachActivationAsync(ActivateOrDeactivateUserCommand request, ApplicationUser user, string currentUserId)
    {
        var coach = await _unitOfWork.Coaches.FindAsync(c => c.ApplicationUserId == currentUserId, [nameof(Coach.AthleteCoaches)]);
        if (coach == null)
            return false;

        if (!request.IsDeactivating)
        {
            user.IsActive = true;
        }
        else
        {
            if (!CoachHasActiveSubscribtions(coach.AthleteCoaches))
            {
                user.IsActive = false;
            }
            else
            {
                throw new BadRequestException(new[] { new ValidationError { Field = "IsActive", Message = "Account can't be deactivated while having active subscriptions." } });
            }
        }

        return true;
    }

    private bool CoachHasActiveSubscribtions(ICollection<AthleteCoach> coachAthletes)
    {
        return coachAthletes != null && coachAthletes.Any(ca => ca.IsCurrentlySubscribed);
    }
}
