using Application.CQRS.Command.CourseCycles;
using Application.CQRS.Query.CourseCycles;
using Contract.Dto.CourseCycles;
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
    public class CourseCycleController : ControllerBase
    {

        private readonly IMediator mediator;
        private readonly UserManager<User> userManager;

        public CourseCycleController(IMediator mediator, UserManager<User> userManager)
        {
            this.mediator = mediator;
            this.userManager = userManager;
        }

        [HttpPost]
        [Route("CreateCourseCycle")] //CreateCourseCycleCommand
        public async Task<ActionResult> CreateCourseCycle([FromBody] CourseCycleDto courseCycleDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var command = new CreateCourseCycleCommand { CourseCycleDto = courseCycleDto };
                Result result = await mediator.Send(command);

                return result.IsSuccess ? Ok("CourseCycle Added Sucessfully") : BadRequest(result.Error);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("GetCourseCycles")]
        public async Task<ActionResult> GetALlCourseCycles()
        {
            try
            {
                var result = await mediator.Send(new GetAllCourseCyclesQuery());
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
        [Route("UpdateCourseCycle")]
        public async Task<ActionResult> UpdateCourseCycle([FromHeader] int Id, [FromBody] CourseCycleDto courseCycleDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                Result<CourseCycle> resultOfUpdated = await mediator.Send(new UpdateCourseCycleCommand { Id = Id, CourseCycleDto = courseCycleDto });

                return resultOfUpdated.IsSuccess ? Ok(resultOfUpdated.Value) : BadRequest(resultOfUpdated.Error);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("DeleteCourseCycle")]
        public async Task<ActionResult> DeleteCourseCycle([FromHeader] int Id, [FromBody] CourseCycleDto courseCycleDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (Id == 0)
                return BadRequest("Enter valid ID");
            try
            {
                Result<int> resultOfDeleted = await mediator.Send(new DeleteCourseCycleCommand { Id = Id, CourseCycleDto = courseCycleDto });
                return resultOfDeleted.IsSuccess ? Ok(resultOfDeleted.Value) : BadRequest("invalid data");
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
