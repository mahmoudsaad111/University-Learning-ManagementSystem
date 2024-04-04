using Application.CQRS.Command.PostReplies;
using Contract.Dto.PostReplies;
using Domain.Models;
using Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostReplyController : ControllerBase
    {
        private readonly IMediator mediator;

        public PostReplyController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        [Route("CreatePostReply")] //CreatePostReplyCommand
        public async Task<ActionResult> CreatePostReply([FromBody] PostReplyDto postReplyDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var command = new CreatePostReplyCommand { PostReplyDto = postReplyDto };
                Result<PostReply> resultOfCreate = await mediator.Send(command);

                return resultOfCreate.IsSuccess ? Ok("PostReply Added Sucessfully") : BadRequest(resultOfCreate.Error);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("UpdatePostReply")]
        public async Task<ActionResult> UpdatePostReply([FromHeader] int Id, [FromBody] PostReplyDto postReplyDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                Result<PostReply> resultOfUpdated = await mediator.Send(new UpdatePostReplyCommand { Id = Id, PostReplyDto = postReplyDto });

                return resultOfUpdated.IsSuccess ? Ok(resultOfUpdated.Value) : BadRequest(resultOfUpdated.Error);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("DeletePostReply")]
        public async Task<ActionResult> DeletePostReply([FromHeader] int Id, [FromBody] PostReplyDto postReplyDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (Id == 0)
                return BadRequest("Enter valid ID");
            try
            {
                Result<int> resultOfDeleted = await mediator.Send(new DeletePostReplyCommand { Id = Id, PostReplyDto = postReplyDto });
                return resultOfDeleted.IsSuccess ? Ok(resultOfDeleted.Value) : BadRequest("un valid data");
            }
            catch
            {
                return NotFound();
            }
        }
 /*
        [HttpGet]
        [Route("GetPostReplys")]
        public async Task<ActionResult> GetALlPostReplys()
        {
            try
            {
                var result = await mediator.Send(new GetAllPostReplysQuery());
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
    }
}
