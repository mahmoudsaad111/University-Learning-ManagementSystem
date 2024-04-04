using Application.CQRS.Command.AssignementAnswers;
using Application.CQRS.Query.Faculties;
using Contract.Dto.AssignementAnswers;
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
    public class AssignementAnswerController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly UserManager<User> userManager;

        public AssignementAnswerController(IMediator mediator, UserManager<User> userManager)
        {
            this.mediator = mediator;
            this.userManager = userManager;
        }

        [HttpPost]
        [Route("CreateAssignementAnswer")] //CreateAssignementAnswerCommand
        public async Task<ActionResult> CreateAssignementAnswer([FromBody] AssignementAnswerDto assignementAnswerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var command = new CreateAssignementAnswerCommand { AssignementAnswerDto = assignementAnswerDto };
                Result result = await mediator.Send(command);

                return result.IsSuccess ? Ok("AssignementAnswer Added Sucessfully") : BadRequest(result.Error);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        /*
        [HttpGet]
        [Route("GetAssignementAnswers")]
        public async Task<ActionResult> GetALlAssignementAnswers()
        {
            try
            {
                var result = await mediator.Send(new GetAllAssQuery());
                if (result.IsSuccess)
                    return Ok(result.Value);
                return BadRequest(result.Error);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        */
        [HttpPost]
        [Route("UpdateAssignementAnswer")]
        public async Task<ActionResult> UpdateAssignementAnswer([FromHeader] int Id, [FromBody] AssignementAnswerDto assignementAnswerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                Result<AssignmentAnswer> resultOfUpdated = await mediator.Send(new UpdateAssignementAnswerCommand { Id = Id, AssignementAnswerDto = assignementAnswerDto });

                return resultOfUpdated.IsSuccess ? Ok(resultOfUpdated.Value) : BadRequest(resultOfUpdated.Error);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("DeleteAssignementAnswer")]
        public async Task<ActionResult> DeleteAssignementAnswer([FromHeader] int Id, [FromBody] AssignementAnswerDto assignementAnswerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (Id == 0)
                return BadRequest("Enter valid ID");
            try
            {
                Result<int> resultOfDeleted = await mediator.Send(new DeleteAssignementAnswerCommand { Id = Id, AssignementAnswerDto = assignementAnswerDto });
                return resultOfDeleted.IsSuccess ? Ok(resultOfDeleted.Value) : BadRequest("un valid data");
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
