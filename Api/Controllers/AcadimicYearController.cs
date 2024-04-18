using Application.CQRS.Command.AcadimicYears;
using Application.CQRS.Query.AcadimicYears;
using Contract.Dto.AcadimicYears;
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
    public class AcadimicYearController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly UserManager<User> userManager;

        public AcadimicYearController(IMediator mediator, UserManager<User> userManager)
        {
            this.mediator = mediator;
            this.userManager = userManager;
        }

        [HttpPost]
        [Route("CreateAcadimicYear")] //CreateAcadimicYearCommand
        public async Task<ActionResult> CreateAcadimicYear([FromBody] AcadimicYearDto AcadimicYearDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var command = new CreateAcadimicYearCommand { AcadimicYearDto = AcadimicYearDto };
                Result result = await mediator.Send(command);
                return result.IsSuccess ? Ok("AcadimicYear Added Sucessfully") : BadRequest(result.Error);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("GetAcadimicYears")]
        public async Task<ActionResult> GetALlAcadimicYears([FromHeader] int DeptId)
        {
            try
            {
                var result = await mediator.Send(new GetAllAcadimicYearsQuery { DepartementId=DeptId });
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
        [Route("UpdateAcadimicYear")]
        public async Task<ActionResult> UpdateAcadimicYear([FromHeader] int Id, [FromBody] AcadimicYearDto AcadimicYearDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                Result<AcadimicYear> resultOfUpdated = await mediator.Send(new UpdateAcadimicYearCommand { Id = Id, AcadimicYearDto = AcadimicYearDto });

                return resultOfUpdated.IsSuccess ? Ok(resultOfUpdated.Value) : BadRequest(resultOfUpdated.Error);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("DeleteAcadimicYear")]
        public async Task<ActionResult> DeleteAcadimicYear([FromHeader] int Id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (Id == 0)
                return BadRequest("Enter valid ID");
            try
            {
                Result<int> resultOfDeleted = await mediator.Send(new DeleteAcadimicYearCommand { Id = Id });
                return resultOfDeleted.IsSuccess ? Ok(resultOfDeleted.Value) : BadRequest("un valid data");
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
