using Application.CQRS.Command.Departements;
using Application.CQRS.Query.Departements;
using Application.CQRS.Query.Faculties;
using Contract.Dto.Departements;
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
    public class DepartementController : ControllerBase
    {


        private readonly IMediator mediator;
        private readonly UserManager<User> userManager;

        public DepartementController(IMediator mediator, UserManager<User> userManager)
        {
            this.mediator = mediator;
            this.userManager = userManager;
        }

        [HttpPost]
        [Route("CreateDepartement")] //CreateDepartementCommand
        public async Task<ActionResult> CreateDepartement([FromBody] DepartementDto departementDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var command = new CreateDepartementCommand { DepartementDto = departementDto };
                Result result = await mediator.Send(command);

                return result.IsSuccess ? Ok("Departement Added Sucessfully") : BadRequest(result.Error);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("GetDepartements")]
        public async Task<ActionResult> GetALlDepartements()
        {
            try
            {
                var result = await mediator.Send(new GetAllDepartementsQuery());
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
        [Route("GetDepartementsOfFaculty")]
        public async Task<ActionResult> GetDepartementsOfFaculty([FromHeader] int FacultyId)
        {
            try
            {
                var result = await mediator.Send(new GetDepartementBelongsToFacultyQuery { FacultyId= FacultyId});
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
        [Route("UpdateDepartement")]
        public async Task<ActionResult> UpdateDepartement([FromHeader] int Id, [FromBody] DepartementDto departementDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                Result<Departement> resultOfUpdated = await mediator.Send(new UpdateDepartementCommand { Id = Id, departementDto = departementDto });

                return resultOfUpdated.IsSuccess ? Ok(resultOfUpdated.Value) : BadRequest(resultOfUpdated.Error);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("DeleteDepartement")]
        public async Task<ActionResult> DeleteDepartement([FromHeader] int Id, [FromBody] DepartementDto departementDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (Id == 0)
                return BadRequest("Enter valid ID");
            try
            {
                Result<int> resultOfDeleted = await mediator.Send(new DeleteDepartementCommand { Id = Id });
                return resultOfDeleted.IsSuccess ? Ok(resultOfDeleted.Value) : BadRequest("un valid data");
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
