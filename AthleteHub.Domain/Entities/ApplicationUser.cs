using AthleteHub.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace AthleteHub.Domain.Entities;

public class ApplicationUser : IdentityUser
{
    public string FirstName {  get; set; }
    public string LastName { get; set; }
    public Gender Gender { get; set; }
    public DateOnly DateOfBirth {  get; set; }
    public string? ProfilePicture {  get; set; }
    public string? Bio {  get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public virtual Athlete Athlete { get; set; }
    public virtual Coach Coach { get; set; }
}
