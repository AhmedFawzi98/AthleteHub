using AthleteHub.Application.Services;
using AthleteHub.Application.Users.Dtos;
using AthleteHub.Domain.Constants;
using AthleteHub.Domain.Entities;
using AthleteHub.Domain.Exceptions;
using AthleteHub.Domain.Interfaces.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics;

namespace AthleteHub.Application.Users.Authenticaion.LoginUser;

public class LoginUserCommanHandler(UserManager<ApplicationUser> _userManager, SignInManager<ApplicationUser> _signInManager,
    ITokenService _tokenService, IUnitOfWork _unitOfWork) : IRequestHandler<LoginUserCommand, UserLoginResponseDto>
{
    public async Task<UserLoginResponseDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.UserNameOrEmail)
            ?? await _userManager.FindByNameAsync(request.UserNameOrEmail);

        var userLoginResponseDto = new UserLoginResponseDto()
        {
            UserNameOrEmail = request.UserNameOrEmail
        };

        if (user == null)
        {
            userLoginResponseDto.IsValidCredentials = false;
            return userLoginResponseDto;
        }

        var IsUserSignedIn = await SignInUserAsync(userLoginResponseDto, user, request);
        if (!IsUserSignedIn)
        {
            return userLoginResponseDto;
        }

        userLoginResponseDto.IsActive = user.IsActive;
        if (!userLoginResponseDto.IsActive)
            return userLoginResponseDto;


        var rolesNames = (await _userManager.GetRolesAsync(user)).ToList();
        userLoginResponseDto.Roles = rolesNames;

        if (await IsCoachAndNotApproved(rolesNames, user))
        {
            userLoginResponseDto.IsApproved = false;
            return userLoginResponseDto;
        }

        await SetAccessTokenAsync(user, userLoginResponseDto, rolesNames);
        await SetRefreshTokenAsync(user, userLoginResponseDto);

        return userLoginResponseDto;
    }

    private async Task<bool> SignInUserAsync(UserLoginResponseDto userLoginResponseDto, ApplicationUser user, LoginUserCommand request)
    {
        var signInAttempResult = await _signInManager.CheckPasswordSignInAsync(user, request.Password, lockoutOnFailure: true);
        userLoginResponseDto.IsValidCredentials = signInAttempResult.Succeeded;
        if (signInAttempResult.IsLockedOut)
        {
            userLoginResponseDto.IsLockedOut = true;
            userLoginResponseDto.LockoutEnd = (await _userManager.GetLockoutEndDateAsync(user)).Value.UtcDateTime;
        }
        if (!signInAttempResult.Succeeded)
            return false;

        if (signInAttempResult.Succeeded)
            await _userManager.ResetAccessFailedCountAsync(user);
        return true;
    }

    private async Task<bool> IsCoachAndNotApproved(List<string> rolesNames, ApplicationUser user)
    {
        if (rolesNames.Contains(RolesConstants.Coach))
        {
            var coach = await _unitOfWork.Coaches.FindAsync(c => c.ApplicationUserId == user.Id);
            if (!coach.IsApproved)
                return true;
        }
        return false;
    }
    private async Task SetAccessTokenAsync(ApplicationUser user, UserLoginResponseDto userLoginResponseDto, List<string> rolesNames)
    {
        var (jwtAccessToken, validTo) = await _tokenService.GetJwtAccessTokenAsync(user, rolesNames);
        userLoginResponseDto.AccessToken = jwtAccessToken;
        userLoginResponseDto.AccessTokenExpiration = validTo;
    }
    private async Task SetRefreshTokenAsync(ApplicationUser user, UserLoginResponseDto userLoginResponseDto)
    {
        var refreshToken = _tokenService.GenerateRefreshToken();
        userLoginResponseDto.RefreshToken = refreshToken.Token;
        userLoginResponseDto.RefreshTokenExpiration = refreshToken.ExpiresOn;

        user.RefreshTokens.Add(refreshToken);
        await _userManager.UpdateAsync(user);

        _tokenService.SaveRefreshTokenInCookie(refreshToken.Token, refreshToken.ExpiresOn);
    }
}
