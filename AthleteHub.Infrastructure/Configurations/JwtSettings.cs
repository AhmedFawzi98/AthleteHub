namespace AthleteHub.Infrastructure.Configurations;

public class JwtSettings
{
    public const string Jwt = "JWT";
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string Secret { get; set; }
    public int AccessTokenExpireDurationInMinutes { get; set; }
    public int RefreshTokenExpireDurationIndays { get; set; }

}
