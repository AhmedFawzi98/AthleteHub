using AthleteHub.Application.Users.Dtos;
using AthleteHub.Domain.Entities;
using AthleteHub.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using static AthleteHub.Domain.Exceptions.BadRequestException;

namespace AthleteHub.Application.Users.ChangeEmail.InitiateChangeEmail;

public class ChangeEmailCommandHandler(IUserContext _userContext, UserManager<ApplicationUser> _userManager) : IRequestHandler<ChangeEmailCommand, ChangeEmailResponseDto>
{
    public async Task<ChangeEmailResponseDto> Handle(ChangeEmailCommand request, CancellationToken cancellationToken)
    {
        var currentUser = _userContext.GetCurrentUser()
           ?? throw new UnAuthorizedException();

        var user = await _userManager.FindByEmailAsync(request.CurrentEmail)
          ?? throw new NotFoundException(nameof(ApplicationUser), currentUser.Id);

        if (user.Email != currentUser.Email)
        {
            throw new BadRequestException(new[]
            {
                    new ValidationError() { Field = nameof(request.CurrentEmail), Message = "Email doesn't match currently logged user email" }
            });
        }

        var token = await _userManager.GenerateChangeEmailTokenAsync(user, request.NewEmail);
        Dictionary<string, string> queryParams = new()
        {
            {"newemail", request.NewEmail},
            {"oldemail", request.CurrentEmail},
            {"token", token }
        };

        string emailChangingconfirmationLink = QueryHelpers.AddQueryString(request.ClientEmailChangingConfirmationUrl, queryParams);

        user.EmailConfirmed = false;
        await _userManager.UpdateAsync(user);

        var response = new ChangeEmailResponseDto()
        {
            EmailConfirmationLink = emailChangingconfirmationLink,
            UserEmailToConfirm = request.NewEmail,
            UserOldEmail = request.CurrentEmail
        };

        return response;
    }
}
