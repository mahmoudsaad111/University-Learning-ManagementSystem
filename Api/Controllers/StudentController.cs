
using Application.Common.Interfaces.Presistance;
using Application.CQRS.Command.Students;
using Application.CQRS.Query.Students;
using Contract.Dto.ReturnedDtos;
using Contract.Dto.UsersDeleteDto;
using Contract.Dto.UsersRegisterDtos;
using Contract.Dto.UserUpdatedDto;
using Domain.Enums;
using Domain.Models;
using Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class StudentController : ControllerBase
	{

		private readonly IMediator mediator;
		private readonly UserManager<User> userManager;
		private readonly IUnitOfwork unitOfwork;
        public StudentController(IMediator mediator, UserManager<User> userManager, IUnitOfwork unitOfwork)
        {
            this.mediator = mediator;
            this.userManager = userManager;
            this.unitOfwork = unitOfwork;
        }
        [HttpPost]
		[Route("CreateStudent")]
		public async Task<ActionResult> CreateStudent([FromBody] StudentRegisterDto studentRegisterDto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);
			try
			{
				User user = studentRegisterDto.GetUser();
				if (User is null)
					return BadRequest(studentRegisterDto);
				user.CreatedAt= DateTime.Now;
				user.UpdatedAt= DateTime.Now;

				IdentityResult result = await userManager.CreateAsync(user, studentRegisterDto.Password);
				if (!result.Succeeded)
					return BadRequest("This Email or user name are used before");
				studentRegisterDto.Id = user.Id;

				try
				{
                    // New line that add the role to the user
                    await userManager.AddToRoleAsync(user, TypesOfUsers.Student.ToString());

                    var command = new CreateStudentCommand() { StudentRegisterDto = studentRegisterDto };
					Result<StudentRegisterDto> commandResult = await mediator.Send(command);
					
					if( commandResult.IsSuccess) {
						await unitOfwork.SaveChangesAsync();
                        return Ok("Student Added sucessfully"); 
					}
					else throw new Exception();
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
		[Route("UpdateStudent")]
		public async Task<ActionResult> UpdateStudent([FromBody] StudentUpdatedDto studentUpdatedDto)
		{
			if (!ModelState.IsValid)
				return BadRequest(studentUpdatedDto);
			try
			{
				User OldUserInfo = await userManager.FindByEmailAsync(studentUpdatedDto.Email);
				if (OldUserInfo is null)
					return BadRequest("there is no student have this email");

				#region TakeTheUnchangedProperitiesFromOldToNewUser

				OldUserInfo.PhoneNumber = studentUpdatedDto.PhoneNumber;
				OldUserInfo.FirstName = studentUpdatedDto.FirstName;
				OldUserInfo.SecondName = studentUpdatedDto.SecondName;
				OldUserInfo.ThirdName = studentUpdatedDto.ThirdName;
				OldUserInfo.FourthName = studentUpdatedDto.FourthName;
				OldUserInfo.Address = studentUpdatedDto.Address;
				OldUserInfo.Gender = studentUpdatedDto.Gender;
				OldUserInfo.BirthDay = studentUpdatedDto.BirthDay;
				OldUserInfo.PhoneNumber = studentUpdatedDto.PhoneNumber;

                #endregion TakeTheUnchangedProperitiesFromOldToNewUser
                OldUserInfo.UpdatedAt = DateTime.Now;
                IdentityResult resultOfUpdateUser = await userManager.UpdateAsync(OldUserInfo);

				if (!resultOfUpdateUser.Succeeded)
					return BadRequest("Unable to update Info");

				studentUpdatedDto.Id= OldUserInfo.Id;
				Result resultOfUpdateStudent = await mediator.Send(new UpdateStudentByIdCommand { Id=OldUserInfo.Id, NewStudentInfo = studentUpdatedDto   });

				if (resultOfUpdateStudent.IsSuccess)
					return Ok("Student updated successfully");
				return BadRequest();
			}
			catch (Exception ex)
			{
				return NotFound();
			}
		}

		[HttpDelete]
		[Route("DeleteStudent")]
		public async Task<ActionResult> DeleteStudent([FromHeader] string Email  )
		{
			if (!ModelState.IsValid)
				return BadRequest("Enter falid email");
			try
			{
				User userDelte = await userManager.FindByEmailAsync( Email);
				if (userDelte is null)
					return BadRequest("No student has this email");
				IdentityResult resultOfDelete= await userManager.DeleteAsync(userDelte);
			
				return  resultOfDelete.Succeeded ? Ok("Student Deleted Succeasfully") :  NotFound();
			}
			catch (Exception ex)
			{
				return NotFound();
			}
		}

		[HttpGet]
		[Route("GetAllStudents")]
		public async Task<ActionResult> GetAllStudents()
		{
			try
			{
				Result<List<ReturnedStudentDto>> resultOfQuery =await mediator.Send(new GetAllStudentsQuery { });
				return resultOfQuery.IsSuccess ? Ok(resultOfQuery.Value) : BadRequest("Uable to load students");
			}
			catch (Exception ex)
			{
				return NoContent() ;
			}
		}

        [HttpGet]
        [Route("GetAllStudentsInDepartementandAcadimicYear")]
        public async Task<ActionResult> GetAllStudentsInDepartementandAcadimicYear([FromHeader] int AcadimicYearId)
        {
            try
            {
                Result<IEnumerable<ReturnedStudentDto>> resultOfQuery = await mediator.Send(new GetAllStudentsOfDepartementAndAcadimicYearQuery { AcadimicYearId=AcadimicYearId });
                return resultOfQuery.IsSuccess ? Ok(resultOfQuery.Value) : BadRequest("Uable to load students");
            }
            catch (Exception ex)
            {
                return NoContent();
            }
        }
    }
}



/*
	   [HttpPost]
	   [Route("UpdateStudent")]
	   public async Task<ActionResult> UpdateStudent([FromBody] StudentRegisterDto studentRegisterDto)
	   {
		   if (!ModelState.IsValid)
			   return BadRequest(studentRegisterDto);
		   try
		   {
			   User NewUserInfo = studentRegisterDto.GetUser();
			   if (NewUserInfo is null)
				   return BadRequest("there is no student have these info");
			   User OldUserInfo = await userManager.FindByEmailAsync(NewUserInfo.Email);
			   if (OldUserInfo is null)
				   return BadRequest("there is no student have this email");

			   Student NewStudentInfo = studentRegisterDto.GetStudent();

			   #region TakeTheUnchangedProperitiesFromOldToNewUser
			   NewUserInfo.Id = OldUserInfo.Id;
			   NewUserInfo.NormalizedEmail = OldUserInfo.NormalizedEmail;
			   NewUserInfo.EmailConfirmed = OldUserInfo.EmailConfirmed;
			   NewUserInfo.PhoneNumberConfirmed = OldUserInfo.PhoneNumberConfirmed;
			   NewUserInfo.SecurityStamp = OldUserInfo.SecurityStamp;
			   NewUserInfo.ConcurrencyStamp = OldUserInfo.ConcurrencyStamp;
			   #endregion TakeTheUnchangedProperitiesFromOldToNewUser


			   IdentityResult resultOfUpdateUser = await userManager.UpdateAsync(NewUserInfo);

			   if (!resultOfUpdateUser.Succeeded)
				   return BadRequest("Unable to update Info");

			   Result resultOfUpdateStudent = await mediator.Send(new UpdateStudentByIdCommand { StudentId = OldUserInfo.Id, NewStudentInfo = NewStudentInfo });

			   if (resultOfUpdateStudent.IsSuccess)
				   return Ok("Student updated successfully");
			   return BadRequest();

		   }
		   catch (Exception ex)
		   {
			   return NotFound();
		   }
	   }
*/        