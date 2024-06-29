using AthleteHub.Application.Features.Queries.GetAllFeatures;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AthleteHub.Api.Controllers
{
    [Route("api")]
    [ApiController]
    public class FeatureController(IMediator _mediator) : ControllerBase
    {
        [HttpGet("features")]
        public async Task<IActionResult> GetNotApprovedCoaches()
        {
            var featuresDtos = await _mediator.Send(new GetAllFeaturesQuery());
            return Ok(featuresDtos);
        }
    }
}
