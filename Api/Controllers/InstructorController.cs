using Application.Common.Interfaces.Presistance;
using Application.CQRS.Command.Instructors;
 
using Application.CQRS.Query.Instructors;
using Application.CQRS.Query.Professors;
using Contract.Dto.ReturnedDtos;
using Contract.Dto.UsersDeleteDto;
using Contract.Dto.UsersRegisterDtos;
using Contract.Dto.UserUpdatedDto;
using Domain.Models;
using Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class InstructorController : ControllerBase
	{

		private readonly IMediator mediator;
		private readonly UserManager<User> userManager;
        private readonly IUnitOfwork unitOfwork;
        public InstructorController(IMediator mediator, UserManager<User> userManager, IUnitOfwork unitOfwork)
        {
            this.mediator = mediator;
            this.userManager = userManager;
            this.unitOfwork = unitOfwork;
        }

        [HttpPost]
		[Route("CreateInstructor")]
		public async Task<ActionResult> CreateInstructor([FromBody] InstructorRegisterDto instructorRegisterDto)
		{
			if (!ModelState.IsValid)
				return BadRequest(instructorRegisterDto);
			try
			{
				User user = instructorRegisterDto.GetUser();
				if (User is null)
					return BadRequest(instructorRegisterDto);

                user.CreatedAt = DateTime.Now;
                user.UpdatedAt = DateTime.Now;
                IdentityResult result = await userManager.CreateAsync(user, instructorRegisterDto.Password);
				if (!result.Succeeded)
					return BadRequest("This Email or user name are used before");

				instructorRegisterDto.Id = user.Id;

				try
				{
					var command = new CreateInstructorCommand() { InstructorRegisterDto = instructorRegisterDto };
					Result<InstructorRegisterDto> commandResult = await mediator.Send(command);
					return commandResult.IsSuccess ? Ok("Instructor Added sucessfully") : throw new Exception();
				}
				catch (Exception ex)
				{
                    await userManager.DeleteAsync(user);
                    await unitOfwork.SaveChangesAsync();
                    return BadRequest("Invalid Data");
                }
			}
			catch
			{
				return BadRequest();
			}
		}



		[HttpPut]
		[Route("UpdateInstructor")]
		public async Task<ActionResult> UpdateInstructor([FromBody] InstructorUpdateDto instructorUpdatedDto)
		{
			if (!ModelState.IsValid)
				return BadRequest(instructorUpdatedDto);
			try
			{
				User OldUserInfo = await userManager.FindByEmailAsync(instructorUpdatedDto.Email);
				if (OldUserInfo is null)
					return BadRequest("there is no instructor have this email");

				#region TakeTheUnchangedProperitiesFromOldToNewUser

				OldUserInfo.PhoneNumber = instructorUpdatedDto.PhoneNumber;
				OldUserInfo.FirstName = instructorUpdatedDto.FirstName;
				OldUserInfo.SecondName = instructorUpdatedDto.SecondName;
				OldUserInfo.ThirdName = instructorUpdatedDto.ThirdName;
				OldUserInfo.FourthName = instructorUpdatedDto.FourthName;
				OldUserInfo.Address = instructorUpdatedDto.Address;
				OldUserInfo.Gender = instructorUpdatedDto.Gender;
				OldUserInfo.BirthDay = instructorUpdatedDto.BirthDay;
				OldUserInfo.PhoneNumber = instructorUpdatedDto.PhoneNumber;

				#endregion TakeTheUnchangedProperitiesFromOldToNewUser
				OldUserInfo.UpdatedAt = DateTime.Now;

				IdentityResult resultOfUpdateUser = await userManager.UpdateAsync(OldUserInfo);

				if (!resultOfUpdateUser.Succeeded)
					return BadRequest("Unable to update Info");

				instructorUpdatedDto.Id = OldUserInfo.Id;
				Result resultOfUpdateInstructor = await mediator.Send(new UpdateInstructorByIdCommand { Id = OldUserInfo.Id, NewInstructorInfo = instructorUpdatedDto });

				if (resultOfUpdateInstructor.IsSuccess)
					return Ok("Instructor updated successfully");
				return BadRequest();
			}
			catch (Exception ex)
			{
				return NotFound();
			}
		}

		[HttpDelete]
		[Route("DeleteInstructor")]
		public async Task<ActionResult> DeleteStudent([FromHeader] string Email)
		{
			if (!ModelState.IsValid)
				return BadRequest("Enter falid email");
			try
			{
				User userDelte = await userManager.FindByEmailAsync(Email);
				if (userDelte is null)
					return BadRequest("No Instructor has this email");
				IdentityResult resultOfDelete = await userManager.DeleteAsync(userDelte);

				return resultOfDelete.Succeeded ? Ok("Instructor Deleted Succeasfully") : NotFound();
			}
			catch (Exception ex)
			{
				return NotFound();
			}
		}
		[HttpGet]
		[Route("GetAllInstructors")]
		public async Task<ActionResult> GetAllInstructors()
		{
			try
			{
				Result<List<ReturnedInstructorDto>> resultOfQuery = await mediator.Send(new GetAllInstructorsQuery { });
				return resultOfQuery.IsSuccess ? Ok(resultOfQuery.Value) : BadRequest("Uable to load Instructors");
			}
			catch (Exception ex)
			{
				return NoContent();
			}
		}



        [HttpGet]
        [Route("GetAllInstructorsInDepartement")]
        public async Task<ActionResult> GetAllInstructorsInDepartement([FromHeader] int DepartementId)
        {
            try
            {
                Result<IEnumerable<ReturnedInstructorDto>> resultOfQuery = await mediator.Send(new GetAllInstructorsOfDepartementQuery { DepartementId = DepartementId });
                return resultOfQuery.IsSuccess ? Ok(resultOfQuery.Value) : BadRequest("Uable to load Professors");
            }
            catch (Exception ex)
            {
                return NoContent();
            }
        }


        [HttpGet]
        [Route("GetAllInstructorsLessInfoInDepartement")]
        public async Task<ActionResult> GetAllInstructorsLessInfoInDepartement([FromHeader] int DepartementId)
        {
            try
            {
                var resultOfQuery = await mediator.Send(new GetNameIdInstructorsQuery { DepartementId = DepartementId });
                return resultOfQuery.IsSuccess ? Ok(resultOfQuery.Value) : BadRequest("Uable to load Professors");
            }
            catch (Exception ex)
            {
                return NoContent();
            }
        }
    }
}
