using Api.Pages;
using Application.Common.Interfaces.MailService;
using Application.CQRS.Command.Assignements;
using Application.CQRS.Command.Faculties;
using Application.CQRS.Query.Assignements;
using Application.CQRS.Query.Faculties;
using Application.CQRS.Query.Sections;
using Contract.Dto.Assignements;
using Contract.Dto.Faculties;
using Domain.Enums;
using Domain.Models;
using Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssignementController : ControllerBase
    {
        private readonly IMailingService _mailingService;
        private readonly IRazorPartialToStringRenderer _renderer;
        private readonly IMediator mediator;
        private readonly UserManager<User> userManager;

        public AssignementController(IMediator mediator, UserManager<User> userManager,
            IMailingService mailingService, IRazorPartialToStringRenderer renderer)
        {
            this.mediator = mediator;
            this.userManager = userManager;
            _mailingService = mailingService;   
            _renderer = renderer;   
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
                var result = await mediator.Send(command);

                if (result.IsFailure) return BadRequest(result.Error); 
                // Email notification for students

                var Assignment = result.Value;
                await SendAssignmentNotificationViaEmail(Assignment,"New Assignment"); 

                //////////////////////////////////////

                return result.IsSuccess ? Ok(Assignment) : BadRequest(result.Error);
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
                // Email notification for an update in assigment
                var Assignment = resultOfUpdated.Value;
                await SendAssignmentNotificationViaEmail(Assignment, "Assignment Updated");
                ///////////////////////

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

        async Task SendAssignmentNotificationViaEmail(Assignment Assignment,string TypeOfEmail) {
            var EmailsResult = await mediator.Send(new GetEmailsOfStudnetsHavingAccessToAssignmentQuery { AssignmentId = Assignment.AssignmentId });
            var Emails = EmailsResult.Value;
            var AssignmentLink = $"http://localhost:3000/Assignement/GetAllFilesForAssignemnt?AssignmentId={Assignment.AssignmentId}";
            var SectionAndCourseCycleName = await mediator.Send(new GetSectionAndCourseCycleNameQuery { SectionId = Assignment.SectionId });
            var model = new AssigmentsNotificationModel
            {
                TypeOfEmail = TypeOfEmail,  
                AssignmentLink = AssignmentLink,
                Deadline = Assignment.EndedAt,
                SectionName = SectionAndCourseCycleName.Value.SectionName,
                CourseCycleName = SectionAndCourseCycleName.Value.CourseCycleName
            };

            var htmlContent = await _renderer.RenderPartialToStringAsync("AssignmentsNotificationTemplate", model);

            await _mailingService.SendToEmails(Emails, "Assignment Notigication", htmlContent);

        }
    }
}
