using Contract.Dto.UsersRegisterDtos;
using Domain.Enums;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : Controller
    {
        private readonly UserManager<User> userManager;
        public AdminController(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        [HttpPost]
        [Route("CreateAdmin")]
        public async Task<ActionResult> CreateAdmin([FromBody] AdminRegisterDto adminRegisterDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                User user = adminRegisterDto.GetUser();

                if (User is null)
                    return BadRequest(adminRegisterDto);

                user.CreatedAt = DateTime.Now;
                user.UpdatedAt = DateTime.Now;

                IdentityResult result = await userManager.CreateAsync(user, adminRegisterDto.Password);
                if (!result.Succeeded)
                    return BadRequest("This Email or user name are used before");
                adminRegisterDto.Id = user.Id;

         
                // New line that add the role to the user
                await userManager.AddToRoleAsync(user, TypesOfUsers.Admin.ToString());
                return Ok("Admin Added sucessfully");
             
              
            }
            catch
            {
                return BadRequest();
            }
        }

    }
}
