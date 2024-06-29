using AthleteHub.Domain.Entities;
using AthleteHub.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AthleteHub.Application.Users.Authenticaion.ConfirmEmail;

public class ConfirmChangeEmailCommandHandler(UserManager<ApplicationUser> _userManager) : IRequestHandler<ConfirmChangeEmailCommand>
{
    public async Task Handle(ConfirmChangeEmailCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.UserOldEmail)
            ?? throw new NotFoundException(nameof(ApplicationUser), request.UserOldEmail);

        var result = await _userManager.ChangeEmailAsync(user, request.UserEmailToConfirm, request.Token);
        if(!result.Succeeded)
        {
            throw new BadRequestException(new[]
               {
                new BadRequestException.ValidationError()
                {
                    Field = request.Token,
                    Message = "Couldn't validate user token"
                }
            });
        }

        user.EmailConfirmed = true;
        await _userManager.UpdateAsync(user);
    }
}
