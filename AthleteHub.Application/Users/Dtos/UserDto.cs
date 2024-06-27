using AthleteHub.Domain.Enums;

namespace AthleteHub.Application.Users.Dtos;

public class UserDto
{
    public string Id {  get; set; }
    public string Email { get; set; }
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public Gender Gender { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string? ProfilePicture { get; set; }
    public string? Bio { get; set; }
    public List<string>? Roles { get; set; }
}