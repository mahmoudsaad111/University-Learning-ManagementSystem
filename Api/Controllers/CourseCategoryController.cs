using Application.CQRS.Command.CourseCategories;
using Application.CQRS.Query.CourseCategories;
using Contract.Dto.CourseCategories;
using Domain.Models;
using Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseCategoryController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly UserManager<User> userManager;

        public CourseCategoryController(IMediator mediator, UserManager<User> userManager)
        {
            this.mediator = mediator;
            this.userManager = userManager;
        }

        [HttpPost]
        [Route("CreateCourseCategory")] //CreateCourseCategoryCommand
        public async Task<ActionResult> CreateCourseCategory([FromBody] CourseCategoryDto courseCategoryDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var command = new CreateCourseCategoryCommand { CourseCategoryDto = courseCategoryDto };
                Result result = await mediator.Send(command);

                return result.IsSuccess ? Ok("CourseCategory Added Sucessfully") : BadRequest(result.Error);
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
             var result = await mediator.Send(new GetAllCourseCategoriesQuery());
             if (result.IsSuccess)
                 return Ok(result.Value);
             return BadRequest(result.Error);
        }

        [HttpPost]
        [Route("UpdateCourseCategory")]
        public async Task<ActionResult> UpdateCourseCategory([FromHeader] int Id, [FromBody] CourseCategoryDto courseCategoryDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
 
            Result<CourseCategory> resultOfUpdated = await mediator.Send(new UpdateCourseCategoryCommand { Id = Id, CourseCategoryDto = courseCategoryDto });
            return resultOfUpdated.IsSuccess ? Ok(resultOfUpdated.Value) : BadRequest(resultOfUpdated.Error);      
        }

        [HttpPost]
        [Route("DeleteCourseCategory")]
        public async Task<ActionResult> DeleteCourseCategory([FromHeader] int Id, [FromBody] CourseCategoryDto courseCategoryDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (Id == 0)
                return BadRequest("Enter valid ID");
         
            Result<int> resultOfDeleted = await mediator.Send(new DeleteCourseCategoryCommand { Id = Id, CourseCategoryDto = courseCategoryDto });
            return resultOfDeleted.IsSuccess ? Ok(resultOfDeleted.Value) : BadRequest("un valid data");
        }
    }
}

