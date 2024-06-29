using AthleteHub.Application.Users;
using AthleteHub.Domain.Constants;
using AthleteHub.Domain.Entities;
using AthleteHub.Domain.Exceptions;
using AthleteHub.Domain.Interfaces.Repositories;
using MediatR;


namespace AthleteHub.Application.Admin.Commands.CoachStatus
{
    public class CoachStatusCommandHandler(IUnitOfWork _unitOfWork, IUserContext _userContext) : IRequestHandler<CoachStatusCommand, string>
    {
        public async Task<string> Handle(CoachStatusCommand request, CancellationToken cancellationToken)
        {
            var currentUser = _userContext.GetCurrentUser();
            if (!currentUser.IsInRole(RolesConstants.Admin))
                throw new UnAuthorizedException();

            var coach = await _unitOfWork.Coaches.FindAsync(c => c.Id == request.CoachId, new() { { c => c.ApplicationUser, new(null, null) } });

            if (coach == null)
                throw new NotFoundException(nameof(Coach), request.CoachId.ToString());

            coach.IsSuspended = request.IsSuspend;
            _unitOfWork.Coaches.Update(coach);
            await _unitOfWork.CommitAsync();
            return coach?.ApplicationUser.Email ?? null;
        }
    }
}
