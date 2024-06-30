using Application.CQRS.Command.Lectures;
using Application.CQRS.Query.Lectures;
using Contract.Dto.Lectures;
using Domain.Models;
using Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Authorize]

    [Route("api/[controller]")]
    [ApiController]
    public class LectureController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly UserManager<User> userManager;

        public LectureController(IMediator mediator, UserManager<User> userManager)
        {
            this.mediator = mediator;
            this.userManager = userManager;
        }

        [HttpPost]
        [Route("GetLectureComments")] //CreateLectureCommand
        public async Task<ActionResult> GetLectureComments([FromBody]  int LectureId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var query = new GetLectureCommentsQuery { LectureId = LectureId };
                var result = await mediator.Send(query);

                return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("CreateLecture")] //CreateLectureCommand
        public async Task<ActionResult> CreateLecture([FromBody] LectureDto lectureDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var command = new CreateLectureCommand { LectureDto = lectureDto };
                Result result = await mediator.Send(command);

                return result.IsSuccess ? Ok("Lecture Added Sucessfully") : BadRequest(result.Error);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("GetLectures")]
        public async Task<ActionResult> GetALlLectures()
        {
            try
            {
                var result = await mediator.Send(new GetAllLecturesQuery());
                if (result.IsSuccess)
                    return Ok(result.Value);
                return BadRequest(result.Error);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut]
        [Route("UpdateLecture")]
        public async Task<ActionResult> UpdateLecture([FromHeader] int Id, [FromBody] LectureDto lectureDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                Result<Lecture> resultOfUpdated = await mediator.Send(new UpdateLectureCommand { Id = Id, LectureDto = lectureDto });

                return resultOfUpdated.IsSuccess ? Ok(resultOfUpdated.Value) : BadRequest(resultOfUpdated.Error);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [HttpDelete]
        [Route("DeleteLecture")]
        public async Task<ActionResult> DeleteLecture([FromHeader] int Id )
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (Id == 0)
                return BadRequest("Enter valid ID");
            try
            {
                Result<int> resultOfDeleted = await mediator.Send(new DeleteLectureCommand { Id = Id });
                return resultOfDeleted.IsSuccess ? Ok(resultOfDeleted.Value) : BadRequest("un valid data");
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
