using AthleteHub.Domain.Entities;
using AthleteHub.Domain.Interfaces.Repositories;
using AthleteHub.Application.Subscribtions.Commands.CreateSubscribtion;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AthleteHub.Application.Subscribtions.Queries.FindSubscribtion;
using AthleteHub.Application.Subscribtions.Dtos;
using Microsoft.AspNetCore.Authorization;
using AthleteHub.Domain.Constants;

namespace AthleteHub.Api.Controllers
{
    [Route("api")]
    [ApiController]
    public class SubscribtionController : ControllerBase
    {
        private readonly IMediator _mediator;
        public SubscribtionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("subscribtions")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SubscribtionDto>))]
        public async Task<IActionResult> GetAllSubscribtions([FromQuery] GetAllSubscribtionsQuery getAllSubscribtionsQuery)
        {
            var subscribtionsDtos = await _mediator.Send(getAllSubscribtionsQuery);
            return Ok(subscribtionsDtos);
        }

        [HttpGet("subscribtions/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SubscribtionDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> GetSubscribtionById(int id, [FromQuery] bool includes)
        {
            var subscribtionDto = await _mediator.Send(new FindSubscribtionQuery {  Id = id, Includes = includes, Criteria = res => res.Id == id });
            return Ok(subscribtionDto);
        }


        [HttpPost("subscribtions")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SubscribtionDto))]
        [Authorize(Roles = RolesConstants.Coach)]
        public async Task<IActionResult> AddSubscribtion(CreateSubscribtionCommand createSubscribtionCommand)
        {
            var addedSubscribtion = await _mediator.Send(createSubscribtionCommand);
            return CreatedAtAction(nameof(GetSubscribtionById), new {addedSubscribtion.Id}, addedSubscribtion);
        }
    }
}
