using Application.CQRS.Command.CommentReplies;
using Contract.Dto.CommentReplies;
using Domain.Models;
using Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Authorize]

    [Route("api/[controller]")]
    [ApiController]
    public class CommentReplyController : ControllerBase
    {

        private readonly IMediator mediator;

        public CommentReplyController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        [Route("CreateCommentReply")] //CreateCommentReplyCommand
        public async Task<ActionResult> CreateCommentReply([FromBody] CommentReplyDto commentReplyDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var command = new CreateCommentReplyCommand { commentReplyDto = commentReplyDto };
                Result<CommentReply> resultOfCreate = await mediator.Send(command);

                return resultOfCreate.IsSuccess ? Ok("CommentReply Added Sucessfully") : BadRequest(resultOfCreate.Error);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut]
        [Route("UpdateCommentReply")]
        public async Task<ActionResult> UpdateCommentReply([FromHeader] int Id, [FromBody] CommentReplyDto commentReplyDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                Result<CommentReply> resultOfUpdated = await mediator.Send(new UpdateCommentReplyCommand { Id = Id, CommentReplyDto = commentReplyDto });

                return resultOfUpdated.IsSuccess ? Ok(resultOfUpdated.Value) : BadRequest(resultOfUpdated.Error);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [HttpDelete]
        [Route("DeleteCommentReply")]
        public async Task<ActionResult> DeleteCommentReply([FromHeader] int Id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (Id == 0)
                return BadRequest("Enter valid ID");
            try
            {
                Result<int> resultOfDeleted = await mediator.Send(new DeleteCommentReplyCommand { Id = Id });
                return resultOfDeleted.IsSuccess ? Ok(resultOfDeleted.Value) : BadRequest("un valid data");
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
