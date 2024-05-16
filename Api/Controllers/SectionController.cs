using Application.Common.Interfaces.Presistance;
using Application.CQRS.Command.Sections;
using Application.CQRS.Query.Courses;
using Application.CQRS.Query.Instructors;
using Application.CQRS.Query.Professors;
using Application.CQRS.Query.Sections;
using Application.CQRS.Query.Students;
using Contract.Dto.Sections;
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
    public class SectionController : ControllerBase
    {

        private readonly IMediator mediator;
        private readonly UserManager<User> userManager;
       
        public SectionController(IMediator mediator, UserManager<User> userManager )
        {
            this.mediator = mediator;
            this.userManager = userManager;
    
        }

        [HttpPost]
        [Route("CreateSection")] //CreateSectionCommand
        public async Task<ActionResult> CreateSection([FromBody] SectionDto sectionDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var command = new CreateSectionCommand { SectionDto = sectionDto };
                Result result = await mediator.Send(command);

                return result.IsSuccess ? Ok("Section Added Sucessfully") : BadRequest(result.Error);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("GetSections")]
        public async Task<ActionResult> GetALlSections()
        {
            try
            {

                var result = await mediator.Send(new GetAllSectionsQuery());
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
        [Route("GetSectionOfCourseToStudent")]
        public async Task<ActionResult> GetSectionOfCourseToStudent([FromHeader] string StudentUserName, [FromHeader] int CourseCylceId)
        {
            try
            {
                var user = await userManager.FindByNameAsync(StudentUserName);
                if (user is null)
                    return BadRequest("Wrong userName");

                var ResultOfGetStudent = await mediator.Send(new GetStudentByIdQuery { Id = user.Id });

                if (ResultOfGetStudent is null || ResultOfGetStudent.IsFailure || ResultOfGetStudent.Value is null)
                    return BadRequest("Wrong userName2");

                var Student = ResultOfGetStudent.Value;

                var result = await mediator.Send(new GetSectionOfCourseToStudentQuery { StudentId = Student.StudentId, CourseCycleId = CourseCylceId }) ;
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
        [Route("GetAllSectionsOfInstructor")]
        public async Task<ActionResult> GetAllSectionsOfInstructor([FromHeader] string InstructorUserName)
        {
            try
            {
                var user = await userManager.FindByNameAsync(InstructorUserName);
                if(user is null )
                    return BadRequest("Wrong userName");

                var ResultOfInstructor = await mediator.Send(new GetInstructorByIdQuery { Id=user.Id });

                if (ResultOfInstructor is null || ResultOfInstructor.IsFailure || ResultOfInstructor.Value is null)
                    return BadRequest("Wrong userName");

                var Instructor = ResultOfInstructor.Value;

                var result = await mediator.Send(new GetAllSectionsOfInstructorQuery { InstructorId = Instructor.InstructorId }) ;
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
        [Route("GetAllSectionsOfCourseCycle")]
        public async Task<ActionResult> GetAllSectionsOfCourseCycle([FromHeader] int CourseCycleId)
        {
            try
            {                
                if (CourseCycleId == 0 )
                    return BadRequest("Wrong Id");


                var result = await mediator.Send(new GetAllSectionsOfCourseCycleQuery { CourseCycleId = CourseCycleId }) ;
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
        [Route("GetAllSectionsOfProfessor")]
        public async Task<ActionResult> GetAllSectionsOfProfessor([FromHeader] string ProfessorUserName)
        {
            try
            {
                var user = await userManager.FindByNameAsync(ProfessorUserName);
                if (user is null)
                    return BadRequest("Wrong userName");

                var ResultOfProfessor = await mediator.Send(new GetProfessorByIdQuery { Id = user.Id });

                if (ResultOfProfessor is null || ResultOfProfessor.IsFailure || ResultOfProfessor.Value is null)
                    return BadRequest("Wrong userName");

                var Professor = ResultOfProfessor.Value;

                var result = await mediator.Send(new GetAllSectionsOfProfessorQuery { ProfessorId = Professor.ProfessorId });
                if (result.IsSuccess)
                    return Ok(result.Value);
                return BadRequest(result.Error);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);

            }
        }

        [HttpGet("GetAllSectionsOfStudnet")]
        public async Task<ActionResult> GetAllSectionsOfStudnet(string StudnetUserName)
        {
            if (StudnetUserName is null)
                return BadRequest();
            try
            {
                var Result = await mediator.Send(new GetAllSectionsToStudentQuery { StudentUserName = StudnetUserName });   
                return Result.IsSuccess ? Ok(Result.Value) : BadRequest(Result.Error);  
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }



        [HttpPut]
        [Route("UpdateSection")]
        public async Task<ActionResult> UpdateSection([FromHeader] int Id, [FromBody] SectionDto sectionDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                Result<Section> resultOfUpdated = await mediator.Send(new UpdateSectionCommand { Id = Id, SectionDto = sectionDto });

                return resultOfUpdated.IsSuccess ? Ok(resultOfUpdated.Value) : BadRequest(resultOfUpdated.Error);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [HttpDelete]
        [Route("DeleteSection")]
        public async Task<ActionResult> DeleteSection([FromHeader] int Id )
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (Id == 0)
                return BadRequest("Enter valid ID");
            try
            {
                Result<int> resultOfDeleted = await mediator.Send(new DeleteSectionCommand { Id = Id });
                return resultOfDeleted.IsSuccess ? Ok(resultOfDeleted.Value) : BadRequest("un valid data");
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
