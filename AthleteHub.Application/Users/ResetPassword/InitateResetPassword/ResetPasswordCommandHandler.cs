using AthleteHub.Application.Users.Dtos;
using AthleteHub.Domain.Entities;
using AthleteHub.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.WebUtilities;

namespace AthleteHub.Application.Users.ResetPassword.InitateResetPassword;

public class ResetPasswordCommandHandler(UserManager<ApplicationUser> _userManager) : IRequestHandler<ResetPasswordCommand, ResetPasswordResponseDto>
{
    public async Task<ResetPasswordResponseDto> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email)
            ?? throw new NotFoundException(nameof(ApplicationUser), request.Email);

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        Dictionary<string, string> queryParams = new()
        {
            {"email",request.Email},
            {"token", token}
        };

        string passwordResetLink = QueryHelpers.AddQueryString(request.ClientResetPasswordUrl, queryParams);

        var resetPasswordResonseDto = new ResetPasswordResponseDto()
        {
            UserEmailToConfirm = request.Email,
            PasswordResetLink = passwordResetLink
        };

        return resetPasswordResonseDto;
    }
}
