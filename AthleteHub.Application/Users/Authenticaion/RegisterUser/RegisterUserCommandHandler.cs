using AthleteHub.Application.Users.Dtos;
using AthleteHub.Domain.Constants;
using AthleteHub.Domain.Entities;
using AthleteHub.Domain.Exceptions;
using AthleteHub.Domain.Interfaces.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using static AthleteHub.Domain.Exceptions.BadRequestException;

namespace AthleteHub.Application.Users.Authenticaion.RegisterUser;

public class ActivateOrDeactivateUserCommandHandler(IMapper _mapper, UserManager<ApplicationUser> _userManager
    , IUnitOfWork _unitOfWork) : IRequestHandler<RegisterUserCommand, UserDto>
{
    public async Task<UserDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        await EnsureUniqueEmailandUsername(request.Email, request.UserName);

        var user = _mapper.Map<ApplicationUser>(request);

        var registerResult = await _userManager.CreateAsync(user, request.Password);
        if (!registerResult.Succeeded)
        {
            var errors = registerResult.Errors.Select(e => new ValidationError() { Field = e.Code, Message = e.Description }).ToList();
            throw new BadRequestException(errors);
        }
        else
        {
            var userDto = _mapper.Map<UserDto>(user);

            if (request.IsCoach)
            {
                var coachId = await CreateCoachAsync(user.Id);
                userDto.EntityId = coachId;
                userDto.Roles = [RolesConstants.Coach];
                await _userManager.AddToRoleAsync(user, RolesConstants.Coach);
            }
            else
            {
                var athleteId = await CreateAthleteAsync(user.Id, (decimal)request.Height!);
                userDto.EntityId = athleteId;
                userDto.Roles = [RolesConstants.Athlete];
                await _userManager.AddToRoleAsync(user, RolesConstants.Athlete);
            }

            return userDto;
        }
    }
    private async Task EnsureUniqueEmailandUsername(string email, string username)
    {
        var userWithSameEmail = await _userManager.FindByEmailAsync(email);
        if (userWithSameEmail != null)
        {
            throw new BadRequestException(new[]
            {
                    new ValidationError() { Field = nameof(userWithSameEmail.Email), Message = "Email address already exists." }
            });
        }

        var userWithSameUsername = await _userManager.FindByNameAsync(username);
        if (userWithSameUsername != null)
        {
            throw new BadRequestException(new[]
            {
                    new ValidationError() { Field = nameof(userWithSameUsername.UserName), Message = "UserName already exists." }
            });
        }
    }
    private async Task<int> CreateCoachAsync(string applicationuserId)
    {
        var coach = new Coach() { ApplicationUserId = applicationuserId };
        await _unitOfWork.Coaches.AddAsync(coach);
        await _unitOfWork.CommitAsync();
        return coach.Id;
    }
    private async Task<int> CreateAthleteAsync(string applicationuserId, decimal height)
    {
        var athlete = new Athlete() { ApplicationUserId = applicationuserId, HeightInCm = height };
        await _unitOfWork.Athletes.AddAsync(athlete);
        await _unitOfWork.CommitAsync();
        return athlete.Id;
    }
}
