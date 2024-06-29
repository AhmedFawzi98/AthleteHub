using AthleteHub.Domain.Entities;
using AthleteHub.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AthleteHub.Application.Users.ResetPassword.ConfirmResetPassword;

public class ConfirmResetPasswordCommandHandler(UserManager<ApplicationUser> _userManager) : IRequestHandler<ConfirmResetPasswordCommand>
{
    public async Task Handle(ConfirmResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email)
            ?? throw new NotFoundException(nameof(ApplicationUser), request.Email);

        var result = await _userManager.ResetPasswordAsync(user, request.Token, request.NewPassword);
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
        if(user.LockoutEnd != null && user.LockoutEnd.Value >= DateTimeOffset.UtcNow)
        {
            user.LockoutEnd = null;
            await _userManager.UpdateAsync(user);
        }
    }
}
