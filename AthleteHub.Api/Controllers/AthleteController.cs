﻿using AthleteHub.Application.Athletes.Commands.AddToFavourite;
using AthleteHub.Application.Athletes.Commands.CalCalculatecalory;
using AthleteHub.Application.Athletes.Commands.CheckSubscribeAblity;
using AthleteHub.Application.Athletes.Commands.CreateMeasurement;
using AthleteHub.Application.Athletes.Commands.DeleteMeasurement;
using AthleteHub.Application.Athletes.Commands.RateCoach;
using AthleteHub.Application.Athletes.Commands.Subscribe;
using AthleteHub.Application.Athletes.Dtos;
using AthleteHub.Application.Athletes.Queries.FindAthleteById;
using AthleteHub.Application.Athletes.Queries.FindMeasurement;

using AthleteHub.Application.Athletes.Queries.GetAllfavoriteCoaches;
using AthleteHub.Application.Athletes.Queries.GetAllMeasurement;
using AthleteHub.Application.Athletes.Queries.GetAthleteSubscribtion;
using AthleteHub.Application.Coaches.Dtoes;
using AthleteHub.Application.Coaches.Queries.GetAllCoaches;

using AthleteHub.Application.Athletes.Queries.GetAllAthletes;

using AthleteHub.Domain.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
        [HttpGet("athletes")]
        [Authorize(Roles =$"{RolesConstants.Admin},{RolesConstants.Coach}")]
        public async Task<IActionResult> GetAllAthletes([FromQuery]GetAllAthletesQuery getAllAthletesQuery)
        {
            var allAthleteDto = await _mediator.Send(getAllAthletesQuery);
            return Ok(allAthleteDto);
        }
        [HttpGet("athletes/{id:int}")]
        public async Task<IActionResult> GetAthleteById(int id)
        {
            var athleteDto = await _mediator.Send(new FindAthleteByIDQuery { Id = id });  
            return Ok(athleteDto);
        }

        [HttpPost("Subscribe")]
        public async Task<IActionResult> Subscribe(SubscribeCommand subscribeCommand)
        {
            var subscribeResponseDto = await _mediator.Send(subscribeCommand);
            return Ok(subscribeResponseDto);
        }
        [HttpGet("CheckSubscribeAblity")]
        public async Task<IActionResult> CheckSubscribeAblity([FromQuery]CheckSubscribeAblityCommand checkSubscribeAblityCommand)
        {
            var checkSubscribeAblityResponseDto = await _mediator.Send(checkSubscribeAblityCommand);
            return Ok(checkSubscribeAblityResponseDto);
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

        [HttpPost("Athlete/RateCoach")]
        public async Task<IActionResult> RateCoach(RateCoachCommand rateCoachCommand)
        {
            var rateCoachResponseDto = await _mediator.Send(rateCoachCommand);
            return Ok(rateCoachResponseDto);
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
