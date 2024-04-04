using Application.Common.Interfaces.FileProcessing;
using Application.CQRS.Command.FilesProcessing;
using Contract.Dto.Files;
using Contract.Dto.UsersDeleteDto;
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
    public class UserController : ControllerBase
    {

        private readonly UserManager<User> userManager;
        private readonly IMediator mediator;
        string hostUrl = "";
        public UserController(UserManager<User> userManager, IMediator mediator)
        {
            this.userManager = userManager;
            this.mediator = mediator;
        }

        [HttpPost("UpdatePersonalPhotoToUser")]
        public async Task<ActionResult> UpdatePersonalPictureOfUser([FromForm] UploadImageToUserDto UploadImageToUserDto)
        {
            if (!ModelState.IsValid)
                return BadRequest("Enter falid email");
            try
            {
                hostUrl = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
            
                User user = await userManager.FindByNameAsync(UploadImageToUserDto.UserName);       
                if (user is null)
                    return BadRequest("No student has this email");

                
                UploadImageToUserDto.Name = user.UserName;
                var result =await mediator.Send(new UploadImageToUserCommand { UploadImageToUserDto= UploadImageToUserDto });
                if (result.IsSuccess)
                {
                    hostUrl += "//" + result.Value;
                    return Ok(hostUrl);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
    }
}
