﻿using AthleteHub.Application.Admin.Commands.ApproveCoach;
using AthleteHub.Application.Admin.Commands.CoachStatus;
using AthleteHub.Application.Admin.Commands.DeclineCoach;
using AthleteHub.Application.Admin.Queries.GetAllSuspendedOrNotApprovedCoaches;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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

       
    }
}
