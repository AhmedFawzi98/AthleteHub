using AthleteHub.Application.Athletes.Commands.AddToFavourite;
using AthleteHub.Application.Athletes.Commands.CalCalculatecalory;
using AthleteHub.Application.Athletes.Commands.Subscribe;
using AthleteHub.Domain.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace AthleteHub.Api.Controllers
{
    [Route("api")]
    [ApiController]
    public class AthleteController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AthleteController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("Subscribe")]
        public async Task<IActionResult> Subscribe(SubscribeCommand subscribeCommand)
        {
            var athleteActiveSubscribtionDto = await _mediator.Send(subscribeCommand);
            return Ok(athleteActiveSubscribtionDto);
        }

        [HttpPost("AddToFavorite")]
        public async Task<IActionResult> AddToFavorite(AddToFavouriteCommand addToFavouriteCommand)
        {
            var athletefavouritecoachDto = await _mediator.Send(addToFavouriteCommand);
            return Ok(athletefavouritecoachDto);
        }
        [HttpPost("CalCalculatecalory")]
        public async Task<IActionResult> CalCalculatecalory(CalCalculatecaloryCommand calCalculatecaloryCommand)
        {
            var tdee = await _mediator.Send(calCalculatecaloryCommand);
            return Ok(tdee);
        }
    }
}
