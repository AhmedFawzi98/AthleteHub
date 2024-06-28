using AthleteHub.Api.Dtos;
using AthleteHub.Application.Users.ActivateAndDeactivateUser;
using AthleteHub.Application.Users.Authenticaion.ConfirmEmail;
using AthleteHub.Application.Users.Authenticaion.LoginUser;
using AthleteHub.Application.Users.Authenticaion.RefreshUserToken;
using AthleteHub.Application.Users.Authenticaion.RegisterUser;
using AthleteHub.Application.Users.Authenticaion.RevokeUserToken;
using AthleteHub.Application.Users.ChangeEmail.InitiateChangeEmail;
using AthleteHub.Application.Users.ChangePassword;
using AthleteHub.Application.Users.Dtos;
using AthleteHub.Application.Users.GetUser;
using AthleteHub.Application.Users.ResetPassword.ConfirmResetPassword;
using AthleteHub.Application.Users.ResetPassword.InitateResetPassword;
using AthleteHub.Application.Users.UpdateUser;
using Azure;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace AthleteHub.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class IdentityController(IMediator _mediator) : ControllerBase
{
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EmailConfirmationResponseDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDto))]
    public async Task<IActionResult> Register([FromForm] RegisterUserCommand registerUserCommand)
    {
        var ResponseDto = await _mediator.Send(registerUserCommand);
        return Ok(ResponseDto);
    }

    [HttpGet("confirmEmail")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDto))]
    public async Task<IActionResult> ConfirmEmail([FromQuery] ConfirmEmailCommand confirmEmailCommand)
    {
        await _mediator.Send(confirmEmailCommand);
        return NoContent();
    }

    [HttpGet("getUserById/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    public async Task<IActionResult> GetUserById([FromRoute] string id)
    {
        var command = new GetUserCommand() { ApplicationUserId = id};
        var userDto = await _mediator.Send(command);
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

    [HttpPatch("changePassword")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
    [Authorize]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommand changePasswordCommand)
    {
        await _mediator.Send(changePasswordCommand);
        return NoContent();
    }

    [HttpGet("changeEmail")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EmailConfirmationResponseDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    [Authorize]
    public async Task<IActionResult> ChangeEmail([FromBody] ChangeEmailCommand changeEmailCommand)
    {
        var response = await _mediator.Send(changeEmailCommand);
        return Ok(response);
    }

    [HttpGet("confirmChangeEmail")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    [Authorize]
    public async Task<IActionResult> ConfirmChangeEmail([FromQuery] ConfirmChangeEmailCommand confirmChangeEmailCommand)
    {
        await _mediator.Send(confirmChangeEmailCommand);
        return NoContent();
    }


    [HttpGet("resetPassword")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResetPasswordResponseDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommand resetPasswordCommand)
    {
        var response = await _mediator.Send(resetPasswordCommand);
        return NoContent();
    }

    [HttpGet("confirmresetPassword")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    public async Task<IActionResult> ConfirmResetPassword([FromQuery] ConfirmResetPasswordCommand confirmResetPasswordCommand)
    {
        await _mediator.Send(confirmResetPasswordCommand);
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
    public async Task<IActionResult> RevokeToken()
    {
        var revokeTokenCommand = new RevokeTokenCommand();
        var revokeTokenResponseDto = await _mediator.Send(revokeTokenCommand);
        return Ok(revokeTokenResponseDto);
    }
}
