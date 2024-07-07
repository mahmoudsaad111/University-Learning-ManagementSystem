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
  //  [Authorize]

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

        [HttpGet]
        [Route("GetLectureComments")] 
        public async Task<ActionResult> GetLectureComments([FromHeader]  int LectureId)
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

        [ HttpPost]
        [Route("CreateLecture")] //CreateLectureCommand
        public async Task<ActionResult> CreateLecture([FromBody] LectureDto lectureDto , bool IsProfessor , string CreatorUserNanme)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var command = new CreateLectureCommand { LectureDto = lectureDto, CreatorUserName = CreatorUserNanme, IsProfessor = IsProfessor };
                Result<Lecture> result = await mediator.Send(command);
                 
                return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpGet]
        [Route("GetLecturesForStudent")]
        public async Task<ActionResult> GetALlLectures( int CourseCycleId, string StudentUserName, int SectionId) 
        {
            try
            {
                var result = await mediator.Send(
                    new GetAllLecturesForStudentQuery
                    {
                        getLectureForStudentDto = new GetLectureForStudentDto
                        {
                            CourseCycleId = CourseCycleId,
                            SectionId = SectionId,
                            StudentUserName = StudentUserName
                        }
                    });
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
        [Route("GetLecturesForCreator")]
        public async Task<ActionResult> GetALlLectures(bool IsProfessor, int CourseCycleId , string CreatorUserName , int SectionId) 
        {
            try
            {
                var result = await mediator.Send(
                    new GetAllLecturesForCreatroQuery { 
                        GetLectureDto = new GetLectureForCreatorDto
                        {
                            CourseCycleId = CourseCycleId,
                            SectionId = SectionId,
                            CreatorUserName = CreatorUserName,
                            IsProfessor = IsProfessor
                        }
                });
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
        public async Task<ActionResult> DeleteLecture([FromHeader] int Id , string CreatorUserName)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (Id == 0)
                return BadRequest("Enter valid ID");
            try
            {
                Result<int> resultOfDeleted = await mediator.Send(new DeleteLectureCommand { Id = Id, CreatorUserName = CreatorUserName });
                return resultOfDeleted.IsSuccess ? Ok(resultOfDeleted.Value) : BadRequest("un valid data");
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
