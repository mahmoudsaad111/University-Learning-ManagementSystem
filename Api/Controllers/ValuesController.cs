using Application.Common.Interfaces.Presistance;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {

        private readonly UserManager<User> userManager;
        private readonly IMediator mediator;
        private readonly IUnitOfwork unitOfWork;
        string hostUrl = "";
        public ValuesController(UserManager<User> userManager, IMediator mediator, IUnitOfwork unitOfWork)
        {
            this.userManager = userManager;
            this.mediator = mediator;
            this.unitOfWork = unitOfWork;
        }

        [HttpGet("intructorSection")]
        public async Task<ActionResult> intructorSection(string  instructorUserName)
        {
           var user= await userManager.FindByNameAsync(instructorUserName);

            var result = await unitOfWork.SectionRepository.GetSectionsOfInstructor(user.Id);
            return Ok(result);  
        }
    }
}
