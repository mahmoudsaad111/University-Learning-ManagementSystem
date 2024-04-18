using Application.CQRS.Command.Faculties;
using Application.CQRS.Query.Faculties;
using Contract.Dto.Faculties;
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
	public class FacultyController : ControllerBase
	{
		private readonly IMediator mediator;
		private readonly UserManager<User> userManager;

		public FacultyController(IMediator mediator, UserManager<User> userManager)
		{
			this.mediator = mediator;
			this.userManager = userManager;
		}

		[HttpPost]
		[Route("CreateFaculty")] //CreateFacultyCommand
		public async Task<ActionResult> CreateFaculty([FromBody] FacultyDto facultyDto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);
			try
			{
				var command = new CreateFacultyCommand { FacultyDto = facultyDto };
				Result result = await mediator.Send(command);

				return result.IsSuccess ? Ok("Faculty Added Sucessfully") : BadRequest(result.Error);
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
		}      

		[HttpGet]
		[Route("GetFaculties")]
		public async Task<ActionResult> GetALlFaculties()
		{
			try
			{
				var result  = await mediator.Send(new GetAllFacultiesQuery());
				if (result.IsSuccess)
					return Ok(result.Value);
				return BadRequest(result.Error);				
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
		}


        [HttpGet]
        [Route("GetFacultiesLessInfo")]
        public async Task<ActionResult> GetFacultiesLessInfo()
        {
            try
            {
                var result = await mediator.Send(new GetLessInfoAllFacultiesQuery());
                if (result.IsSuccess)
                    return Ok(result.Value);
                return BadRequest(result.Error);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpPost]
		[Route("UpdateFaculty")]
		public async Task<ActionResult> UpdateFaculty([FromHeader] int Id, [FromBody] FacultyDto facultyDto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);
			try
			{
				Result<Faculty> resultOfUpdated =await mediator.Send(new UpdateFacultyCommand { Id = Id, facultyDto = facultyDto });

				return resultOfUpdated.IsSuccess ? Ok(resultOfUpdated.Value) : BadRequest(resultOfUpdated.Error) ;		
			}
			catch (Exception ex)
			{
				 return NotFound();
			}
		}

		[HttpPost]
		[Route("DeleteFaculty")]
		public async Task<ActionResult> DeleteFaculty([FromHeader] int Id, [FromBody] FacultyDto facultyDto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);
			if (Id == 0)
				return BadRequest("Enter valid ID");
			try
			{
				Result<int> resultOfDeleted = await mediator.Send(new DeleteFacultyCommand { Id = Id });
				return resultOfDeleted.IsSuccess ? Ok(resultOfDeleted.Value) : BadRequest("un valid data");
			}
			catch
			{
				return NotFound();	
			}
		}
	}
}
