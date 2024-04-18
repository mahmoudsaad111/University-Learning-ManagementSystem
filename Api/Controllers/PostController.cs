
using Application.CQRS.Command.Posts;
using Application.CQRS.Query.Posts;
using Contract.Dto.Posts;
using Domain.Models;
using Domain.Shared;
using MediatR;
 using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly UserManager<User> userManager;

        public PostController(IMediator mediator, UserManager<User> userManager)
        {
            this.mediator = mediator;
            this.userManager = userManager;
        }

        [HttpPost]
        [Route("CreatePost")] //CreatePostCommand
        public async Task<ActionResult> CreatePost([FromBody] PostDto postDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var command = new CreatePostCommand { PostDto = postDto };
                Result result = await mediator.Send(command);

                return result.IsSuccess ? Ok("Post Added Sucessfully") : BadRequest(result.Error);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("GetFaculties")]
        public async Task<ActionResult> GetALlFaculties()
        {
            try
            {
                var result = await mediator.Send(new GetAllPostsQuery());
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
        [Route("UpdatePost")]
        public async Task<ActionResult> UpdatePost([FromHeader] int Id, [FromBody] PostDto postDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                Result<Post> resultOfUpdated = await mediator.Send(new UpdatePostCommand { Id = Id, PostDto = postDto });

                return resultOfUpdated.IsSuccess ? Ok(resultOfUpdated.Value) : BadRequest(resultOfUpdated.Error);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("DeletePost")]
        public async Task<ActionResult> DeletePost([FromHeader] int Id, [FromBody] PostDto postDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (Id == 0)
                return BadRequest("Enter valid ID");
            try
            {
                Result<int> resultOfDeleted = await mediator.Send(new DeletePostCommand { Id = Id });
                return resultOfDeleted.IsSuccess ? Ok(resultOfDeleted.Value) : BadRequest("un valid data");
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
