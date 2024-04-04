 
using Application.CQRS.Command.Professors;
using Application.CQRS.Query.Professors;
using Contract.Dto.ReturnedDtos;
using Contract.Dto.UsersDeleteDto;
using Contract.Dto.UsersRegisterDtos;
using Contract.Dto.UserUpdatedDto;
using Domain.Models;
using Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProfessorController : ControllerBase
	{
		private readonly IMediator mediator;
		private readonly UserManager<User> userManager;		 
		public ProfessorController(IMediator mediator, UserManager<User> userManager)
		{
			this.mediator = mediator;
			this.userManager = userManager;
		}

		[HttpPost]
		[Route("CreateProfessor")]
		public async Task<ActionResult> CreateProfessor([FromBody] ProfessorRegisterDto professorRegisterDto)
		{
			if (!ModelState.IsValid)
				return BadRequest(professorRegisterDto);
			try
			{
				User user = professorRegisterDto.GetUser();
				if (User is null)
					return BadRequest(professorRegisterDto);

				IdentityResult result = await userManager.CreateAsync(user, professorRegisterDto.Password);
				if (!result.Succeeded)
					return BadRequest("This Email or user name are used before");

				professorRegisterDto.Id = user.Id;

				try
				{
					var command = new CreateProfessorCommand() { ProfessorRegisterDto = professorRegisterDto };
					Result<ProfessorRegisterDto> commandResult = await mediator.Send(command);
					return commandResult.IsSuccess ? Ok("Professor Added sucessfully") : throw new Exception();
				}
				catch (Exception ex)
				{
					userManager.DeleteAsync(user);
					return BadRequest("Invalid Date");
				}
			}
			catch
			{
				return BadRequest();
			}
		}

		[HttpPost]
		[Route("UpdateProfessor")]
		public async Task<ActionResult> UpdateProfessor([FromBody] ProfessorUpdatedDto professorUpdatedDto)
		{
			if (!ModelState.IsValid)
				return BadRequest(professorUpdatedDto);
			try
			{
				User OldUserInfo = await userManager.FindByEmailAsync(professorUpdatedDto.Email);
				if (OldUserInfo is null)
					return BadRequest("there is no Professor have this email");

				#region TakeTheUnchangedProperitiesFromOldToNewUser

				OldUserInfo.PhoneNumber = professorUpdatedDto.PhoneNumber;
				OldUserInfo.FirstName = professorUpdatedDto.FirstName;
				OldUserInfo.SecondName = professorUpdatedDto.SecondName;
				OldUserInfo.ThirdName = professorUpdatedDto.ThirdName;
				OldUserInfo.FourthName = professorUpdatedDto.FourthName;
				OldUserInfo.Address = professorUpdatedDto.Address;
				OldUserInfo.Gender = professorUpdatedDto.Gender;
				OldUserInfo.BirthDay = professorUpdatedDto.BirthDay;
				OldUserInfo.PhoneNumber = professorUpdatedDto.PhoneNumber;

				#endregion TakeTheUnchangedProperitiesFromOldToNewUser

				IdentityResult resultOfUpdateUser = await userManager.UpdateAsync(OldUserInfo);

				if (!resultOfUpdateUser.Succeeded)
					return BadRequest("Unable to update Info");

				professorUpdatedDto.Id = OldUserInfo.Id;
				Result resultOfUpdateProfessor = await mediator.Send(new UpdateProfessorByIdCommand { Id = OldUserInfo.Id, NewProfessorInfo = professorUpdatedDto });

				if (resultOfUpdateProfessor.IsSuccess)
					return Ok("Professor updated successfully");
				return BadRequest();
			}
			catch (Exception ex)
			{
				return NotFound();
			}
		}

		[HttpPost]
		[Route("DeleteProfessor")]
		public async Task<ActionResult> DeleteStudent([FromBody] UserDeleteDto userDelteDto)
		{
			if (!ModelState.IsValid)
				return BadRequest("Enter falid email");
			try
			{
				User userDelte = await userManager.FindByEmailAsync(userDelteDto.Email);
				if (userDelte is null)
					return BadRequest("No Professor has this email");
				IdentityResult resultOfDelete = await userManager.DeleteAsync(userDelte);

				return resultOfDelete.Succeeded ? Ok("Professor Deleted Succeasfully") : NotFound();
			}
			catch (Exception ex)
			{
				return NotFound();
			}
		}

		[HttpGet]
		[Route("GetAllProfessors")]
		public async Task<ActionResult> GetAllProfessors()
		{
			try
			{
				Result<List<ReturnedProfessorDto>> resultOfQuery = await mediator.Send(new GetAllProfessorsQuery { });
				return resultOfQuery.IsSuccess ? Ok(resultOfQuery.Value) : BadRequest("Uable to load Professors");
			}
			catch (Exception ex)
			{
				return NoContent();
			}
		}
	}
}
