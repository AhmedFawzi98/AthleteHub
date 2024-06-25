using System.Text.Json.Serialization;

namespace AthleteHub.Application.Users.Dtos;

public class RevokeTokenResponseDto
{
    public bool IsRevoked { get; set; }
}
