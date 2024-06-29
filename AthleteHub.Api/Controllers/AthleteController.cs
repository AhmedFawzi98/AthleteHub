﻿using AthleteHub.Application.Athletes.Commands.AddToFavourite;
using AthleteHub.Application.Athletes.Commands.CalCalculatecalory;
using AthleteHub.Application.Athletes.Commands.CreateMeasurement;
using AthleteHub.Application.Athletes.Commands.DeleteMeasurement;
using AthleteHub.Application.Athletes.Commands.Subscribe;
using AthleteHub.Application.Athletes.Dtos;
using AthleteHub.Application.Athletes.Queries.FindMeasurement;
using AthleteHub.Application.Athletes.Queries.GetAllfavoriteCoaches;
using AthleteHub.Application.Athletes.Queries.GetAllMeasurement;
using AthleteHub.Application.Athletes.Queries.GetAthleteSubscribtion;
using AthleteHub.Application.Coaches.Dtoes;
using AthleteHub.Application.Coaches.Queries.GetAllCoaches;
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


        [HttpPost("Athlete/{AthleteId:int}/Measurement")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MeasurementDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> AddMeasurement(int AthleteId, CreateMeasurementCommand createMeasurementCommand)
        {
            createMeasurementCommand.SetAthleteId(AthleteId);
            var addedMeasurement = await _mediator.Send(createMeasurementCommand);
            return Ok(addedMeasurement);
        }

        [HttpDelete("Athlete/Measurement")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [Authorize(Roles = RolesConstants.Athlete)]

        public async Task<IActionResult> DeleteMeasurement([FromQuery] DateTime date)
        {
            await _mediator.Send(new DeleteMeasurementCommand {Date = DateOnly.FromDateTime(date) });
            return NoContent();
        }
        [HttpGet("Athlete/{AthleteId:int}/Measurement")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MeasurementDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [Authorize(Roles = RolesConstants.Athlete)]
        public async Task<IActionResult> GetMeasurementByAthleteId(int AthleteId, [FromQuery] DateTime date)
        {

            var MeasurementDto = await _mediator.Send(new FindMeasurementQuery { AthleteId = AthleteId, Date = DateOnly.FromDateTime(date) });
            return Ok(MeasurementDto);
        }

        [HttpGet("AllMeasurement")]
       
        public async Task<IActionResult> GetAllMeasurement([FromQuery] GetAllMeasurementssQuery getAllMeasurementssQuery)
        {
            var MeasurementDto = await _mediator.Send(getAllMeasurementssQuery);
            if (MeasurementDto.TotalItemsCount > 0)
                return Ok(MeasurementDto);
            return NotFound("There are no Measurements");

        }

        [HttpGet("FavCoaches")]
        
        public async Task<IActionResult> GetFavCoaches([FromQuery] GetAllfavoriteCoachesQuery getAllCoachesQuery)
        {
            var favCoachesDtos = await _mediator.Send(getAllCoachesQuery);
            if (favCoachesDtos.TotalItemsCount > 0)
                return Ok(favCoachesDtos);
            return NotFound("There are no coaches in favorite");
        }

        [HttpGet("AthleteSubscribtion")]
       
        public async Task<IActionResult> GetAthleteSubscribtion([FromQuery] GetAthleteSubscribtionQuery getAthleteSubscribtionQueryy)
        {
            var AthleteSubscribtionQueryDtos = await _mediator.Send(getAthleteSubscribtionQueryy);
            if (!AthleteSubscribtionQueryDtos.Any())
            {
                return NotFound("No favorite coaches found.");
            }
            return Ok(AthleteSubscribtionQueryDtos);
        }
    }
}
