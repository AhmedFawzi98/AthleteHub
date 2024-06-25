using AthleteHub.Application.Services;
using AthleteHub.Application.Users.Dtos;
using AthleteHub.Domain.Entities;
using AthleteHub.Domain.Exceptions;
using AthleteHub.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Resturants.Application.Users;

namespace AthleteHub.Application.Users.Authenticaion.RefreshUserToken;

public class RevokeTokenCommandHandler(UserManager<ApplicationUser> _userManager, ITokenService _tokenService
    , IUserContext _userContext, IUnitOfWork _unitOfWork) : IRequestHandler<RefreshTokenCommand, RefreshTokenResponseDto>
{
    public async Task<RefreshTokenResponseDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var currentUser = _userContext.GetCurrentUser()
            ?? throw new UnAuthorizedException();

        var refreshTokenResponseDto = new RefreshTokenResponseDto();
        var refreshTokenFromCookie = _tokenService.GetRefreshTokenFromCookie();
        if (refreshTokenFromCookie == null)
        {
            refreshTokenResponseDto.IsRefreshable = false;
            return refreshTokenResponseDto;
        }

        var user = await _userManager.Users.Include(u => u.RefreshTokens)
            .Where(u => u.Id == currentUser.Id && u.RefreshTokens.Any(t => t.Token == refreshTokenFromCookie))
            .FirstOrDefaultAsync();

        var oldRefreshToken = user?.RefreshTokens.FirstOrDefault();
        if (user == null || !oldRefreshToken!.IsActive)
        {
            refreshTokenResponseDto.IsRefreshable = false;
            return refreshTokenResponseDto;
        }

        refreshTokenResponseDto.IsRefreshable = true;

        var rolesNames = currentUser.Roles.ToList();

        await SetAccessTokenAsync(user, refreshTokenResponseDto, rolesNames);
        await SetRefreshTokenAsync(user, refreshTokenResponseDto, oldRefreshToken);

        return refreshTokenResponseDto;
    }
    private async Task SetAccessTokenAsync(ApplicationUser user, RefreshTokenResponseDto refreshTokenResponseDto, List<string> rolesNames)
    {
        var (jwtAccessToken, validTo) = await _tokenService.GetJwtAccessTokenAsync(user, rolesNames);
        refreshTokenResponseDto.AccessToken = jwtAccessToken;
        refreshTokenResponseDto.AccessTokenExpiration = validTo;
    }
    private async Task SetRefreshTokenAsync(ApplicationUser user, RefreshTokenResponseDto refreshTokenResponseDto, RefreshToken oldRefreshToken)
    {
        oldRefreshToken!.RevokedOn = DateTime.UtcNow;

        var refreshToken = _tokenService.GenerateRefreshToken();
        refreshTokenResponseDto.RefreshToken = refreshToken.Token;
        refreshTokenResponseDto.RefreshTokenExpiration = refreshToken.ExpiresOn;

        user.RefreshTokens.Add(refreshToken);
        await _userManager.UpdateAsync(user);

        _tokenService.SaveRefreshTokenInCookie(refreshToken.Token, refreshToken.ExpiresOn);
    }
}
