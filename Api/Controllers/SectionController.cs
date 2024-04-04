using Application.CQRS.Command.Sections;
using Application.CQRS.Query.Sections;
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

        public SectionController(IMediator mediator, UserManager<User> userManager)
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

        [HttpPost]
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

        [HttpPost]
        [Route("DeleteSection")]
        public async Task<ActionResult> DeleteSection([FromHeader] int Id, [FromBody] SectionDto sectionDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (Id == 0)
                return BadRequest("Enter valid ID");
            try
            {
                Result<int> resultOfDeleted = await mediator.Send(new DeleteSectionCommand { Id = Id, SectionDto = sectionDto });
                return resultOfDeleted.IsSuccess ? Ok(resultOfDeleted.Value) : BadRequest("un valid data");
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
