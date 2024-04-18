using Application.CQRS.Command.Courses;
using Application.CQRS.Query.Courses;
using Contract.Dto.Courses;
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
    public class CourseController : ControllerBase
    {

        private readonly IMediator mediator;
        private readonly UserManager<User> userManager;

        public CourseController(IMediator mediator, UserManager<User> userManager)
        {
            this.mediator = mediator;
            this.userManager = userManager;
        }

        [HttpPost]
        [Route("CreateCourse")] //CreateCourseCommand
        public async Task<ActionResult> CreateCourse([FromBody] CourseDto courseDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var command = new CreateCourseCommand { CourseDto = courseDto };
                Result result = await mediator.Send(command);

                return result.IsSuccess ? Ok("Course Added Sucessfully") : BadRequest(result.Error);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("GetCourses")]
        public async Task<ActionResult> GetALlCourses()
        {
            try
            {
                var result = await mediator.Send(new GetAllCoursesQuery());
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
        [Route("UpdateCourse")]
        public async Task<ActionResult> UpdateCourse([FromHeader] int Id, [FromBody] CourseDto courseDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                Result<Course> resultOfUpdated = await mediator.Send(new UpdateCourseCommand { Id = Id, courseDto = courseDto });

                return resultOfUpdated.IsSuccess ? Ok(resultOfUpdated.Value) : BadRequest(resultOfUpdated.Error);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("DeleteCourse")]
        public async Task<ActionResult> DeleteCourse([FromHeader] int Id, [FromBody] CourseDto courseDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (Id == 0)
                return BadRequest("Enter valid ID");
            try
            {
                Result<int> resultOfDeleted = await mediator.Send(new DeleteCourseCommand { Id = Id });
                return resultOfDeleted.IsSuccess ? Ok(resultOfDeleted.Value) : BadRequest("un valid data");
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
