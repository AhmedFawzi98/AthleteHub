using AthleteHub.Application.Coaches.Queries.FindCoach;
using AthleteHub.Application.Coaches.Queries.GetAllCoaches;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AthleteHub.Api.Controllers
{
    [Route("api")]
    [ApiController]
    public class CoachController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CoachController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("coaches")]
        public async Task<IActionResult> GetAllCoaches([FromQuery] GetAllCoachesQuery getAllCoachesQuery)
        {
            var coachesDtos = await _mediator.Send(getAllCoachesQuery);
            if(coachesDtos.TotalItemsCount > 0) 
               return Ok(coachesDtos);
            return NotFound("There are no coaches with that criteria");
        }
        [HttpGet("coaches/{id:int}")]

        public async Task<IActionResult> GetCoachById(int id)
        {
            var coachDto = await _mediator.Send(new FindCoachByIdQuery { Id = id });
            if(coachDto!=null)
               return Ok(coachDto);
            return NotFound("There is no coache with that Id");
        }
    }
}
