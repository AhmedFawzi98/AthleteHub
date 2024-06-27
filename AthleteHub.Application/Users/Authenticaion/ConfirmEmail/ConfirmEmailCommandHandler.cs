using AthleteHub.Domain.Entities;
using AthleteHub.Domain.Exceptions;
using AthleteHub.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AthleteHub.Application.Users.Authenticaion.ConfirmEmail;

public class ConfirmEmailCommandHandler(UserManager<ApplicationUser> _userManager) : IRequestHandler<ConfirmEmailCommand>
{
    public async Task Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.UserEmailToConfirm)
            ?? throw new NotFoundException(nameof(ApplicationUser), request.UserEmailToConfirm);

        var result = await _userManager.ConfirmEmailAsync(user, request.Token);

        if (!result.Succeeded)
            throw new BadRequestException(new[]
            {
                new BadRequestException.ValidationError()
                {
                    Field = request.Token,
                    Message = "Couldn't validate user token"
                }
            });
    }
}
