using AthleteHub.Application.Admin.Queries;
using AthleteHub.Application.Athletes.Commands.CreateMeasurement;
using AthleteHub.Application.Athletes.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AthleteHub.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController(IMediator _mediator) : ControllerBase
    {

        [HttpGet("SiteInfo")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SiteInfoDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> GetSiteInfo()
        {
            var siteInfo = await _mediator.Send(new GetSiteInfoQuery());
            return Ok(siteInfo);
        }

    }
}
