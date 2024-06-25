using System.Text.Json.Serialization;

namespace AthleteHub.Application.Users.Dtos;

public class UserLoginResponseDto
{
    public bool IsValidCredentials { get; set; }
    public bool IsActive { get; set; }
    public bool IsApproved { get; set; } = true;
    public bool IsLockedOut { get; set; }
    public DateTime? LockoutEnd { get; set; }
    public string UserNameOrEmail { get; set; }
    public List<string>? Roles { get; set; }
    public string? AccessToken { get; set; }
    public DateTime? AccessTokenExpiration { get; set; }

    [JsonIgnore] 
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiration { get; set; } 
}
