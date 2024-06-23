using Contract.Dto.UsersRegisterDtos;
using Domain.Enums;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Authorize]

    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : Controller
    {
        private readonly UserManager<User> userManager;
        public StaffController(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }
        [Authorize(Roles ="Admin")]
        [HttpPost]
        [Route("CreateStaff")]
        public async Task<ActionResult> CreateStaff([FromBody] StaffRegisterDto staffRegisterDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                User user = staffRegisterDto.GetUser();

                if (User is null)
                    return BadRequest(staffRegisterDto);

                user.CreatedAt = DateTime.Now;
                user.UpdatedAt = DateTime.Now;

                IdentityResult result = await userManager.CreateAsync(user, staffRegisterDto.Password);
                if (!result.Succeeded)
                    return BadRequest("This Email or user name are used before");
                staffRegisterDto.Id = user.Id;


                // New line that add the role to the user
                await userManager.AddToRoleAsync(user, TypesOfUsers.Staff.ToString());
                return Ok("Staff Added sucessfully");


            }
            catch
            {
                return BadRequest();
            }
        }

    }
}
