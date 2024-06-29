using AthleteHub.Application.Users.Dtos;
using AthleteHub.Application.Users.GetUser;
using AthleteHub.Domain.Entities;
using AthleteHub.Domain.Exceptions;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace AthleteHub.Application.Users;

public class GetUserCommandHandler(UserManager<ApplicationUser> _userManager, IMapper _mapper) : IRequestHandler<GetUserCommand, UserDto>
{
    public async Task<UserDto> Handle(GetUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.ApplicationUserId) 
            ?? throw new NotFoundException(nameof(ApplicationUser), request.ApplicationUserId);

        var userRoles = await _userManager.GetRolesAsync(user);

        var userDto =  _mapper.Map<UserDto>(user);
        userDto.Roles = userRoles.ToList();

        return userDto;
    }
}
