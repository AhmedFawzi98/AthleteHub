using AthleteHub.Application.Constants;
using AthleteHub.Application.Services;
using AthleteHub.Domain.Entities;
using AthleteHub.Infrastructure.Configurations;
using AthleteHub.Infrastructure.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace AthleteHub.Infrastructure.Authorization.Services;

internal class TokenService(UserManager<ApplicationUser> _userManager, IOptions<JwtSettings> jwtSettingsOptions
    , IHttpContextAccessor _httpContextAccessor) : ITokenService
{
    private readonly JwtSettings _jwtSettings = jwtSettingsOptions.Value;
    public async Task<(string,DateTime)> GetJwtAccessTokenAsync(ApplicationUser user, List<string> rolesNames, int entityId)
    {
        List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(AppClaimTypes.EntityId, entityId.ToString()),
            new Claim(ClaimTypes.Name, user.UserName!),
            new Claim(ClaimTypes.Email, user.Email!),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) 
        };
        
        foreach (var roleName in rolesNames)
        {
            claims.Add(new Claim(ClaimTypes.Role, roleName));
        }

        var claimsFromDbTable = await _userManager.GetClaimsAsync(user);
        foreach (var claim in claimsFromDbTable)
        {
            claims.Add(claim);
        }

        SymmetricSecurityKey securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
        SigningCredentials credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);

        JwtSecurityToken jwtAccessToken = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenExpireDurationInMinutes),
            signingCredentials: credentials
        );
        return (new JwtSecurityTokenHandler().WriteToken(jwtAccessToken), jwtAccessToken.ValidTo);
    }
    public RefreshToken GenerateRefreshToken()
    {
        return new RefreshToken
        {
            Token = GenerateRandomKey(),
            CreatedOn = DateTime.UtcNow,
            ExpiresOn = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpireDurationIndays) 
        };
    }
    private string GenerateRandomKey(int keyLength = 32)
    {
        Span<byte> keyBytes = stackalloc byte[keyLength];

        RandomNumberGenerator.Fill(keyBytes);

        string base64key = Convert.ToBase64String(keyBytes);
        string urlSafeKey = WebUtility.UrlEncode(base64key);

        return urlSafeKey;
    }
    public void SaveRefreshTokenInCookie(string refreshToken, DateTime expires)
    {
        CookieOptions options = new CookieOptions
        {
            Expires = expires,
            HttpOnly = true
        };
        _httpContextAccessor?.HttpContext?.Response.Cookies.Append(TokenConstants.RefreshToken, refreshToken, options);
    }
    public string GetRefreshTokenFromCookie()
    {
        bool CookieExists = _httpContextAccessor.HttpContext.Request.Cookies.TryGetValue(TokenConstants.RefreshToken, out string? refreshToken);

        if (CookieExists)
            return refreshToken!;

        return null!;
    }

    public void RevokeToken(RefreshToken refreshToken)
    {
        refreshToken.RevokedOn = DateTime.UtcNow;
        DateTime oldExpireDateToDeleteCookie = ((DateTime)refreshToken.RevokedOn).AddDays(-1);
        SaveRefreshTokenInCookie(refreshToken.Token, oldExpireDateToDeleteCookie);
    }
}
