
﻿using AthleteHub.Application.Admin.Queries;
using AthleteHub.Application.Athletes.Commands.CreateMeasurement;
using AthleteHub.Application.Athletes.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;

﻿using AthleteHub.Application.Admin.Commands.ApproveCoach;
using AthleteHub.Application.Admin.Commands.CoachStatus;
using AthleteHub.Application.Admin.Commands.DeclineCoach;



using Microsoft.AspNetCore.Mvc;
using AthleteHub.Application.Admin.Queries.GetAllSuspendedOrNotApprovedCoaches;

namespace AthleteHub.Api.Controllers
{
    [Route("api")]
    [ApiController]
    public class AdminController(IMediator _mediator) : ControllerBase
    {
        [HttpGet("NotApprovedCoaches/{pageNumber:int}/{pageSize:int}")]
        public async Task<IActionResult> GetNotApprovedCoaches(int pageNumber, int pageSize)
        {
            var notApprovedCoaches = await _mediator.Send(new GetAllSuspendedOrNotApprovedCoachesQuery()
            { PageNumber = pageNumber, PageSize = pageSize, NotApprovedCoaches = true ,SuspendedCoaches=false });
            return Ok(notApprovedCoaches);
        }
        [HttpGet("SuspendedCoaches/{pageNumber:int}/{pageSize:int}")]
        public async Task<IActionResult> GetSuspendedCoaches(int pageNumber, int pageSize)
        {
            var AllSuspendedCoaches = await _mediator.Send(new GetAllSuspendedOrNotApprovedCoachesQuery()
            { PageNumber = pageNumber, PageSize = pageSize, SuspendedCoaches = true, NotApprovedCoaches=false });
            return Ok(AllSuspendedCoaches);
        }

        [HttpPatch("ApproveCoach/{id:int}")]
        public async Task<IActionResult> ApproveCoach(int id)
        {
            var approvalResponseDto=await _mediator.Send(new ApproveCoachCommand(){ CoachId=id});
            return Ok(approvalResponseDto);
        }
        [HttpDelete("DeclineCoach/{id:int}")]
        public async Task<IActionResult> DeclineCoach(int id)
        {
            var approvalResponseDto = await _mediator.Send(new DeclineCoachCommand() { CoachId = id });
            return Ok(approvalResponseDto);
        }
       
        [HttpPatch("CoachStatus")]
        public async Task<IActionResult> ModifyCoachStatus(CoachStatusCommand coachStatusCommand)
        {
            string coachEmail=await _mediator.Send(coachStatusCommand);
            return Ok(coachEmail);
        }
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
