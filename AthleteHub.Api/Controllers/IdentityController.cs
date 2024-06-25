using AthleteHub.Api.Dtos;
using AthleteHub.Application.Users.ActivateAndDeactivateUser;
using AthleteHub.Application.Users.Authenticaion.LoginUser;
using AthleteHub.Application.Users.Authenticaion.RefreshUserToken;
using AthleteHub.Application.Users.Authenticaion.RegisterUser;
using AthleteHub.Application.Users.Authenticaion.RevokeUserToken;
using AthleteHub.Application.Users.ChangePassword;
using AthleteHub.Application.Users.Dtos;
using AthleteHub.Application.Users.UpdateUser;
using AthleteHub.Domain.Constants;
using AthleteHub.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AthleteHub.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class IdentityController(IMediator _mediator) : ControllerBase
{
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDto))]
    public async Task<IActionResult> Register([FromBody] RegisterUserCommand registerUserCommand)
    {
        var userDto = await _mediator.Send(registerUserCommand);
        return Ok(userDto);
    }

    [HttpPatch("updateUser")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdatedUserDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
    [Authorize]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUserCommand updateUserCommand )
    {
        var updatedUserDto = await _mediator.Send(updateUserCommand);
        return Ok(updatedUserDto);
    }

    [HttpPatch("activateOrDeactivateUser")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
    [Authorize]
    public async Task<IActionResult> ActivateOrDeactivateUser([FromBody] ActivateOrDeactivateUserCommand activateOrDeactivateUserCommand)
    {
        await _mediator.Send(activateOrDeactivateUserCommand);
        return NoContent();
    }

    [HttpPatch("changeUserPasswordById")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
    [Authorize]
    public async Task<IActionResult> ChangeUserPasswordById([FromBody] ChangePasswordCommand changePasswordCommand)
    {
        await _mediator.Send(changePasswordCommand);
        return NoContent();
    }

    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserLoginResponseDto))]
    public async Task<IActionResult> Login([FromBody] LoginUserCommand loginUserCommand)
    {
        var LogedInUserDto = await _mediator.Send(loginUserCommand);
        return Ok(LogedInUserDto);
    }

    [HttpPost("refreshToken")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RefreshTokenResponseDto))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
    public async Task<IActionResult> RefreshToken()
    {
        var refreshTokenCommand = new RefreshTokenCommand();
        var refreshTokenResponseDto = await _mediator.Send(refreshTokenCommand);
        return Ok(refreshTokenResponseDto);
    }

    [HttpPost("revokeToken")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RevokeTokenResponseDto))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
    public async Task<IActionResult> Login()
    {
        var revokeTokenCommand = new RevokeTokenCommand();
        var revokeTokenResponseDto = await _mediator.Send(revokeTokenCommand);
        return Ok(revokeTokenResponseDto);
    }
}
