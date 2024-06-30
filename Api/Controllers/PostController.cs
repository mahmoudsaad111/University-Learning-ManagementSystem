
using Application.CQRS.Command.Posts;
using Application.CQRS.Query.Posts;
using Contract.Dto.Posts;
using Contract.Dto.Sections;
using Domain.Models;
using Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Authorize]
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



        [HttpGet]
        [Route("GetSectionPosts")] 
        public async Task<ActionResult> GetSectionPostsWithComments([FromHeader] int SectionId) {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var query = new GetSectionPostsQuery { SectionId = SectionId };
                var result = await mediator.Send(query);

                return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("GetCourseCyclePosts")] //CreatePostCommand
        public async Task<ActionResult> GetCourseCyclepostsWithComments([FromHeader] int CourseCycleId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var query = new GetCourseCyclePostsQuery {  CourseCycleId= CourseCycleId };
                var result = await mediator.Send(query);

                return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
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
        [Route("GetALlPosts")]
        public async Task<ActionResult> GetALlPosts()
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

        [HttpPut]
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

        [HttpDelete]
        [Route("DeletePost")]
        public async Task<ActionResult> DeletePost([FromHeader] int Id )
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
