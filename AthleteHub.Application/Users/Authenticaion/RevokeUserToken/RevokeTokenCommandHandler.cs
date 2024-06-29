using AthleteHub.Application.Services;
using AthleteHub.Application.Users.Dtos;
using AthleteHub.Domain.Entities;
using AthleteHub.Domain.Exceptions;
using AthleteHub.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AthleteHub.Application.Users.Authenticaion.RevokeUserToken;

public class RevokeTokenCommandHandler(UserManager<ApplicationUser> _userManager, ITokenService _tokenService
    , IUserContext _userContext, IUnitOfWork _unitOfWork) : IRequestHandler<RevokeTokenCommand, RevokeTokenResponseDto>
{
    public async Task<RevokeTokenResponseDto> Handle(RevokeTokenCommand request, CancellationToken cancellationToken)
    {
        var currentUser = _userContext.GetCurrentUser()
            ?? throw new UnAuthorizedException();

        var RevokeTokenResponseDto = new RevokeTokenResponseDto();
        var refreshTokenFromCookie = _tokenService.GetRefreshTokenFromCookie();
        if (refreshTokenFromCookie == null)
        {
            RevokeTokenResponseDto.IsRevoked = false;
            return RevokeTokenResponseDto;
        }

        var user = await _userManager.Users.Include(u => u.RefreshTokens)
            .Where(u => u.Id == currentUser.Id && u.RefreshTokens.Any(t => t.Token == refreshTokenFromCookie))
            .FirstOrDefaultAsync();

        var oldRefreshToken = user?.RefreshTokens.FirstOrDefault(t => t.Token == refreshTokenFromCookie);
        if (user == null || !oldRefreshToken!.IsActive)
        {
            RevokeTokenResponseDto.IsRevoked = false;
            return RevokeTokenResponseDto;
        }

        _tokenService.RevokeToken(oldRefreshToken);
        await _userManager.UpdateAsync(user);

        RevokeTokenResponseDto.IsRevoked = true;
        
        return RevokeTokenResponseDto;
    }
}
