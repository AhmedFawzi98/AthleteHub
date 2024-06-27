using AthleteHub.Application.Coaches.Commands.AddContent;
using AthleteHub.Application.Coaches.Commands.UploadCertificate;
using AthleteHub.Application.Coaches.Queries.FindCoach;
using AthleteHub.Application.Coaches.Queries.GetAllCoaches;
using AthleteHub.Domain.Constants;
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

        [HttpPost("coaches/{id:int}/certificate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [Authorize(Roles = RolesConstants.Coach)]
        public async Task<IActionResult> UploadCertificate(int id, [FromForm] UploadCertificateCommand uploadCertificateCommand)
        {
            if (uploadCertificateCommand.File == null || uploadCertificateCommand.File.Length == 0)
                return BadRequest("File not selected");

            uploadCertificateCommand.SetId(id);

            var sasUrlDto = await _mediator.Send(uploadCertificateCommand);

            return Ok(sasUrlDto);
        }

        [HttpPost("coaches/content/upload")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [Authorize(Roles = RolesConstants.Coach)]
        public async Task<IActionResult> AddContent([FromForm] AddContentCommand addContentCommand)
        {
            if (addContentCommand.Files == null || addContentCommand.Files.Count() == 0)
                return BadRequest("No File Selected");

            var addedContentResponseDto = await _mediator.Send(addContentCommand);

            return Ok(addedContentResponseDto);
        }
    }
}
