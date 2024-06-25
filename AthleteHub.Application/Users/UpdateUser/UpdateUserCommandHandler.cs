using AthleteHub.Application.Users.Dtos;
using AthleteHub.Domain.Entities;
using AthleteHub.Domain.Exceptions;
using AthleteHub.Domain.Interfaces.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Resturants.Application.Users;

namespace AthleteHub.Application.Users.UpdateUser;

public class UpdateUserCommandHandler(IMapper _mapper, UserManager<ApplicationUser> _userManager, IUserContext _userContext) : IRequestHandler<UpdateUserCommand, UpdatedUserDto>
{
    public async Task<UpdatedUserDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var currentUser = _userContext.GetCurrentUser()
            ?? throw new UnAuthorizedException();

        var user = await _userManager.FindByIdAsync(currentUser.Id)
            ?? throw new NotFoundException(nameof(ApplicationUser), currentUser.Id!);
        
        _mapper.Map(request, user);

        await _userManager.UpdateAsync(user);

        var updatedUserDto = _mapper.Map<UpdatedUserDto>(user);

        return updatedUserDto;

    }
}
