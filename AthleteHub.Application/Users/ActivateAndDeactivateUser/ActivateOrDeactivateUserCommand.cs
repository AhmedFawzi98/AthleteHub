using MediatR;

namespace AthleteHub.Application.Users.ActivateAndDeactivateUser;

public class ActivateOrDeactivateUserCommand : IRequest
{
    public bool IsDeactivating { get; set; }
}
