using AthleteHub.Api.Dtos;
using AthleteHub.Application.Emails.SendEmail;
using AthleteHub.Application.Users.Authenticaion.RegisterUser;
using AthleteHub.Application.Users.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AthleteHub.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UtilityController(IMediator _mediator) : ControllerBase
{
    [HttpPost("sendEmail")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> SendEmail([FromForm] SendEmailCommand sendEmailCommand)
    {
        await _mediator.Send(sendEmailCommand);
        return NoContent();
    }
}
