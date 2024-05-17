using Application.CQRS.Command.Assignements;
using Application.CQRS.Command.Faculties;
using Application.CQRS.Query.Assignements;
using Application.CQRS.Query.Faculties;
using Contract.Dto.Assignements;
using Contract.Dto.Faculties;
using Domain.Enums;
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
    public class AssignementController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly UserManager<User> userManager;

        public AssignementController(IMediator mediator, UserManager<User> userManager)
        {
            this.mediator = mediator;
            this.userManager = userManager;
        }

        [HttpPost]
        [Route("CreateAssignement")] //CreateAssignementCommand
        public async Task<ActionResult> CreateAssignement([FromBody] AssignementDto assignementDto, string InstructorUserName)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var command = new CreateAssignementCommand { AssignementDto = assignementDto , InstructorUserName= InstructorUserName };
                Result result = await mediator.Send(command);

                return result.IsSuccess ? Ok("Assignement Added Sucessfully") : BadRequest(result.Error);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        
        [HttpGet]
        [Route("GetAllAssignemntsOfSection")]
        public async Task<ActionResult> GetAllAssignemntsOfSection(int SectionId, string UserName ,  TypesOfUsers typesOfUsers)
        {
            try
            {
                var result = await mediator.Send(new GetAllAssignementsOfSectionQuery
                { 
                    assignmentToAnyUserDto = new AssignmentOfSectionToAnyUserDto
                    {
                        SectionId=SectionId ,
                        UserName=UserName,
                        TypeOfUser=typesOfUsers
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
        [Route("GetAllFilesForAssignemnt")]
        public async Task<ActionResult> GetAllFilesForAssignemnt( int AssignmentId, string UserName , TypesOfUsers typesOfUsers)
        {
            try
            {
                var result = await mediator.Send(new GetAssignementsResourceByIdQuery
                {
                    assignmentsResourseToAnyUserDto = new AssignmentsResourseToAnyUserDto
                    {
                        AssignmentId = AssignmentId,
                        UserName = UserName,
                        TypeOfUser = typesOfUsers
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
        [Route("UpdateAssignement")]
        public async Task<ActionResult> UpdateAssignement([FromHeader] int Id, [FromBody] AssignementDto assignementDto, string InstructorUserName)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                Result<Assignment> resultOfUpdated = await mediator.Send(new UpdateAssignementCommand { Id = Id, AssignementDto = assignementDto , InstructorUserName = InstructorUserName });

                return resultOfUpdated.IsSuccess ? Ok(resultOfUpdated.Value) : BadRequest(resultOfUpdated.Error);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [HttpDelete]
        [Route("DeleteAssignement")]
        public async Task<ActionResult> DeleteAssignement([FromHeader] int Id , string InstructorUserName)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (Id == 0)
                return BadRequest("Enter valid ID");
            try
            {
                Result<int> resultOfDeleted = await mediator.Send(new DeleteAssignementCommand { Id = Id , InstructorUserName= InstructorUserName });
                return resultOfDeleted.IsSuccess ? Ok(resultOfDeleted.Value) : BadRequest("un valid data");
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
