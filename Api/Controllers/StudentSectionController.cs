using Application.CQRS.Command.SectionStudents;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentSectionController : ControllerBase
    {
        private readonly IMediator mediator;

        public StudentSectionController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpPost("AddStudentsToSection")]
        public async Task<ActionResult> AddStudentsToSection([FromBody] string[] studentsUserName, [FromHeader] int SectionId)
        {
            if (studentsUserName is null || SectionId == 0)
                return BadRequest("Invalid data") ;

            var Result =await mediator.Send(new AddStudensToSectionCommand {  StudentsUserNames = studentsUserName, SectionId = SectionId });
            if (Result.IsSuccess)
            {
                if (Result.Value.Count() > 0)
                    return BadRequest(Result.Value);

                return Ok("all students added successfully in section");
            }
            return BadRequest( ) ;
        }


        [HttpDelete("DeleteStudentsFromSection")]
        public async Task<ActionResult> DeleteStudentsFromSection([FromBody] string[] studentsUserName, [FromHeader] int SectionId)
        {
            if (studentsUserName is null || SectionId == 0)
                return BadRequest("Invalid data");

            var Result = await mediator.Send(new DeleteStudentsFromSectionCommand { StudentsUserNames = studentsUserName, SectionId = SectionId });
            if (Result.IsSuccess)
            {
                if(Result.Value.Count() > 0)
                    return BadRequest(Result.Value);
                return Ok("all students deleted successfully from section");
            }
            return BadRequest();
        }
    }
}
