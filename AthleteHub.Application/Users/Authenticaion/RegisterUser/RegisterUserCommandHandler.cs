using AthleteHub.Application.Services.EmailService;
using AthleteHub.Application.Users.Dtos;
using AthleteHub.Domain.Constants;
using AthleteHub.Domain.Entities;
using AthleteHub.Domain.Exceptions;
using AthleteHub.Domain.Interfaces.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using static AthleteHub.Domain.Exceptions.BadRequestException;

namespace AthleteHub.Application.Users.Authenticaion.RegisterUser;

public class RegisterUserCommandHandler(IMapper _mapper, UserManager<ApplicationUser> _userManager
    , IUnitOfWork _unitOfWork, IEmailService _emailService) : IRequestHandler<RegisterUserCommand, EmailConfirmationResponseDto>
{
    public async Task<EmailConfirmationResponseDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
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
            var ResponseDto = new EmailConfirmationResponseDto() {UserEmailToConfirm = request.Email };

            if (request.IsCoach)
            {
                await CreateCoachAsync(user.Id);
                ResponseDto.Roles = [RolesConstants.Coach];
                await _userManager.AddToRoleAsync(user, RolesConstants.Coach);
            }
            else
            {
                await CreateAthleteAsync(user.Id, (decimal)request.Height!);
                ResponseDto.Roles = [RolesConstants.Athlete];
                await _userManager.AddToRoleAsync(user, RolesConstants.Athlete);
            }

            ResponseDto.EmailConfirmationLink = await GenerateEmailConfirmationLinkAsync(user, request.ClientEmailConfirmationUrl);

            return ResponseDto;
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
    private async Task CreateCoachAsync(string applicationuserId)
    {
        var coach = new Coach() { ApplicationUserId = applicationuserId };
        await _unitOfWork.Coaches.AddAsync(coach);
        await _unitOfWork.CommitAsync();
    }
    private async Task CreateAthleteAsync(string applicationuserId, decimal height)
    {
        var athlete = new Athlete() { ApplicationUserId = applicationuserId, HeightInCm = height };
        await _unitOfWork.Athletes.AddAsync(athlete);
        await _unitOfWork.CommitAsync();
    }
    private async Task<string> GenerateEmailConfirmationLinkAsync(ApplicationUser user, string clientEmailConfirmationUrl)
    {
        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

        Dictionary<string, string> queryParams = new()
        {
            {"email", user.Email},
            {"token", token }
        };

        string confirmationLink = QueryHelpers.AddQueryString(clientEmailConfirmationUrl, queryParams);

        return confirmationLink;
    }
}
