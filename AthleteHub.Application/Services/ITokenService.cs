using AthleteHub.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace AthleteHub.Application.Services;

public interface ITokenService
{
    RefreshToken GenerateRefreshToken();
    Task<(string, DateTime)> GetJwtAccessTokenAsync(ApplicationUser user, List<string> rolesNames);
    void SaveRefreshTokenInCookie(string refreshToken, DateTime expires);
    string GetRefreshTokenFromCookie();
    void RevokeToken(RefreshToken refreshToken);
}
