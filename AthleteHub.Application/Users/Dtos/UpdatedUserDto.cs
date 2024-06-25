using AthleteHub.Domain.Enums;

namespace AthleteHub.Application.Users.Dtos;

public class UpdatedUserDto
{
    public string? UserName { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public Gender? Gender { get; set; }
    public DateOnly? DateOfBirth { get; set; }
    public string? Bio { get; set; }
}