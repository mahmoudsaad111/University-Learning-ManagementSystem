using Application.CQRS.Command.TFQs;
using Contract.Dto.TFQs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Authorize]

    [Route("api/[controller]")]
    [ApiController]
    public class TFQController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TFQController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("CreateTFQ")] 
        public async Task<ActionResult> CreateTFQ(TFQDto tFQDto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var ResultOfCreate = await _mediator.Send(new CreateTFQCommand { TFQDto = tFQDto }) ;

            if (ResultOfCreate.IsSuccess)
                return Ok(ResultOfCreate.Value) ;
            return BadRequest(ResultOfCreate.Error) ;  
        }
        [HttpDelete("DeleteTFQ")]
        public async Task<ActionResult> DeleteTFQ([FromBody] int TFQId)
        {
            if (TFQId == 0)
                return BadRequest();
            var ResultOfDelete =await _mediator.Send(new DeleteTFQCommand {  TFQId = TFQId }) ;
            if (ResultOfDelete.IsSuccess)
                return Ok(ResultOfDelete.Value);
            return BadRequest(ResultOfDelete.Error);
        }

        [HttpPut("UpdateTFQ")]
        public async Task<ActionResult> UpdateTFQ([FromHeader] int TFQId , TFQDto tFQDto )
        {
            if (TFQId == 0)
                return BadRequest();
            var ResultOfUpdate = await _mediator.Send(new UpdateTFQCommand {TFQDto=tFQDto , TFQId = TFQId });
            if (ResultOfUpdate.IsSuccess)
                return Ok(ResultOfUpdate.Value);
            return BadRequest(ResultOfUpdate.Error);
        }
    }
}
