﻿using Application.CQRS.Command.Groups;
using Application.CQRS.Query.Groups;
using Contract.Dto.Groups;
using Domain.Models;
using Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class GroupController : ControllerBase
	{
		private readonly IMediator mediator;

		public GroupController(IMediator mediator)
		{
			this.mediator = mediator;
		}

		[HttpPost]
		[Route("CreateGroup")] //CreateGroupCommand
		public async Task<ActionResult> CreateGroup([FromBody] GroupDto groupDto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);
			try
			{
				var command = new CreateGroupCommand { groupDto = groupDto };
				Result<Group> resultOfCreate = await mediator.Send(command);

				return resultOfCreate.IsSuccess ? Ok("Group Added Sucessfully") : BadRequest(resultOfCreate.Error);
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
		}

		[HttpPut]
		[Route("UpdateGroup")]
		public async Task<ActionResult> UpdateGroup([FromHeader] int Id, [FromBody] GroupDto groupDto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);
			try
			{
				Result<Group> resultOfUpdated = await mediator.Send(new UpdateGroupCommand { Id = Id, GroupDto = groupDto });

				return resultOfUpdated.IsSuccess ? Ok(resultOfUpdated.Value) : BadRequest(resultOfUpdated.Error);
			}
			catch (Exception ex)
			{
				return NotFound();
			}
		}

		[HttpDelete]
		[Route("DeleteGroup")]
		public async Task<ActionResult> DeleteGroup([FromHeader] int Id )
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);
			if (Id == 0)
				return BadRequest("Enter valid ID");
			try
			{
				Result<int> resultOfDeleted = await mediator.Send(new DeleteGroupCommand { Id = Id });
				return resultOfDeleted.IsSuccess ? Ok(resultOfDeleted.Value) : BadRequest("un valid data");
			}
			catch
			{
				return NotFound();
			}
		}

		[HttpGet]
		[Route("GetGroups")]
		public async Task<ActionResult> GetALlGroups()
		{
			try
			{
				var result = await mediator.Send(new GetAllGroupsQuery());
				if (result.IsSuccess)
					return Ok(result.Value);
				return BadRequest(result.Error);
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
		}
	}
}
