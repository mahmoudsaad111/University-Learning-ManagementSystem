
using Application.Common.Interfaces.Presistance;
using Application.CQRS.Command.Professors;
using Application.CQRS.Query.Professors;
using Contract.Dto.ReturnedDtos;
using Contract.Dto.UsersDeleteDto;
using Contract.Dto.UsersRegisterDtos;
using Contract.Dto.UserUpdatedDto;
using Domain.Enums;
using Domain.Models;
using Domain.Shared;
using Infrastructure.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Authorize]

    [Route("api/[controller]")]
	[ApiController]
	public class ProfessorController : ControllerBase
	{
		private readonly IMediator mediator;
		private readonly UserManager<User> userManager;
        private readonly IUnitOfwork unitOfwork;
        public ProfessorController(IMediator mediator, UserManager<User> userManager, IUnitOfwork unitOfwork)
        {
            this.mediator = mediator;
            this.userManager = userManager;
            this.unitOfwork = unitOfwork;
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
                user.CreatedAt = DateTime.Now;
                user.UpdatedAt = DateTime.Now;
                IdentityResult result = await userManager.CreateAsync(user, professorRegisterDto.Password);
				if (!result.Succeeded)
					return BadRequest("This Email or user name are used before");

				professorRegisterDto.Id = user.Id;
				try
				{
                    // New line that add the role to the user
                    await userManager.AddToRoleAsync(user, TypesOfUsers.Professor.ToString()); 

                    var command = new CreateProfessorCommand() { ProfessorRegisterDto = professorRegisterDto };
					Result<ProfessorRegisterDto> commandResult = await mediator.Send(command);
					return commandResult.IsSuccess ? Ok("Professor Added sucessfully") : throw new Exception();
				}
				catch (Exception ex)
				{
                    await userManager.DeleteAsync(user);
                    await unitOfwork.SaveChangesAsync();
                    return BadRequest("Invalid Data");
                }
			}
			catch(Exception ex) 
			{
				return BadRequest(ex.Message) ;
			}
		}

		[HttpPut]
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

                OldUserInfo.UpdatedAt = DateTime.Now;
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

		[HttpDelete]
		[Route("DeleteProfessor")]
		public async Task<ActionResult> DeleteStudent([FromHeader] string Email  )
		{
			if (!ModelState.IsValid)
				return BadRequest("Enter falid email");
			try
			{
				User userDelte = await userManager.FindByEmailAsync(Email);
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

        [HttpGet]
        [Route("GetAllProfessorsInDepartement")]
        public async Task<ActionResult> GetAllProfessorsInDepartement([FromHeader] int DepartementId)
        {
            try
            {
                Result<IEnumerable<ReturnedProfessorDto>> resultOfQuery = await mediator.Send(new GetAllProfessorsOfDepartementQuery {DepartementId=DepartementId });
                return resultOfQuery.IsSuccess ? Ok(resultOfQuery.Value) : BadRequest("Uable to load Professors");
            }
            catch (Exception ex)
            {
                return NoContent();
            }
        }


        [HttpGet]
        [Route("GetAllProfessorsLessInfoInDepartement")]
        public async Task<ActionResult> GetAllProfessorsLessInfoInDepartement([FromHeader] int DepartementId)
        {
            try
            {
                var resultOfQuery = await mediator.Send(new GetNameIdProfessorsQuery { DepartementId = DepartementId });
                return resultOfQuery.IsSuccess ? Ok(resultOfQuery.Value) : BadRequest("Uable to load Professors");
            }
            catch (Exception ex)
            {
                return NoContent();
            }
        }
    }
}
