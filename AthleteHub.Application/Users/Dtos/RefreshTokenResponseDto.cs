using System.Text.Json.Serialization;

namespace AthleteHub.Application.Users.Dtos;

public class RefreshTokenResponseDto
{
    public bool IsRefreshable { get; set; }
    public string? AccessToken { get; set; }
    public DateTime? AccessTokenExpiration { get; set; }

    [JsonIgnore]
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiration { get; set; }
}
