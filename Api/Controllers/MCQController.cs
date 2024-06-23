using Application.CQRS.Command.MCQs;
using Contract.Dto.MCQs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Authorize]

    [Route("api/[controller]")]
    [ApiController]
    public class MCQController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MCQController(IMediator mediator)
        {
            _mediator = mediator;
        }

 

        [HttpPost("CreateMCQ")]
        public async Task<ActionResult> CreateMCQ(MCQDto mCQDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var ResultOfCreate = await _mediator.Send(new CreateMCQCommand { MCQDto = mCQDto });

            if (ResultOfCreate.IsSuccess)
                return Ok(ResultOfCreate.Value);
            return BadRequest(ResultOfCreate.Error);
        }
        [HttpDelete("DeleteMCQ")]
        public async Task<ActionResult> DeleteMCQ([FromBody] int MCQId)
        {
            if (MCQId == 0)
                return BadRequest();
            var ResultOfDelete = await _mediator.Send(new DeleteMCQCommand { MCQId = MCQId });
            if (ResultOfDelete.IsSuccess)
                return Ok(ResultOfDelete.Value);
            return BadRequest(ResultOfDelete.Error);
        }

        [HttpPut("UpdateMCQ")]
        public async Task<ActionResult> UpdateMCQ([FromHeader] int MCQId, MCQDto mCQDto)
        {
            if (MCQId == 0)
                return BadRequest();
            var ResultOfUpdate = await _mediator.Send(new UpdateMCQCommand { MCQDto = mCQDto, MCQId = MCQId });
            if (ResultOfUpdate.IsSuccess)
                return Ok(ResultOfUpdate.Value);
            return BadRequest(ResultOfUpdate.Error);
        }
    }
}
