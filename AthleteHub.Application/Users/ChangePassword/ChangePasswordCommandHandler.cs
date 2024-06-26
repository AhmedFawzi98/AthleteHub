using AthleteHub.Domain.Entities;
using AthleteHub.Domain.Exceptions;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using static AthleteHub.Domain.Exceptions.BadRequestException;

namespace AthleteHub.Application.Users.ChangePassword;

public class ChangePasswordCommandHandler(IMapper _mapper, UserManager<ApplicationUser> _userManager, IUserContext _userContext): IRequestHandler<ChangePasswordCommand>
{
    public async Task Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var currentUser = _userContext.GetCurrentUser()
            ?? throw new UnAuthorizedException();

        var user = await _userManager.FindByIdAsync(currentUser.Id)
          ?? throw new NotFoundException(nameof(ApplicationUser), currentUser.Id);

        var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(e => new ValidationError() { Field = e.Code, Message = e.Description }).ToList();
            throw new BadRequestException(errors);
        }
    }
}
