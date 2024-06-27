using AthleteHub.Application.Users.Dtos;
using AthleteHub.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace AthleteHub.Application.Users.Authenticaion.RegisterUser;

public class RegisterUserCommand : IRequest<EmailConfirmationResponseDto>
{
    public string Email { get; set; }
    public string ConfirmEmail { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public Gender Gender { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public decimal? Height { get; set; }
    public string? Bio { get; set; }
    public bool IsCoach { get; set; }
    public IFormFile? Certificate { get; set; }
    public string ClientEmailConfirmationUrl {  get; set; }
}
