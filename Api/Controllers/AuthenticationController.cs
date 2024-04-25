using Contract.Authentication;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq.Expressions;
using Application.Common.Interfaces.MailService;
using Application.Common.Interfaces.Authentication;
using LearningPlatformTest.Contracts.Authentication;
using MediatR;
using Application.CQRS.Query.AcadimicYears;
using Application.CQRS.Query.Courses;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IMailingService _mailingService;
        private readonly IMediator _mediator;


        public AuthenticationController(SignInManager<User> signInManager, UserManager<User> userManager
            , IJwtTokenGenerator jwtTokenGenerator, IMailingService mailingService, IMediator mediator)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtTokenGenerator = jwtTokenGenerator;
            _mailingService = mailingService;
            _mediator= mediator;
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginrequest)
        {
            if (!ModelState.IsValid)
                return Unauthorized("Invalid Email Or Password");



            AuthResult authResult = await _jwtTokenGenerator.GenerateToken(loginrequest);

            if (authResult.IsAuthenticated)
                SetRefreshTokenInCookie(authResult.RefreshToken, authResult.RefreshTokenExpiration);
            else
                return Unauthorized("Invalid Email Or Password");


            var user = await _userManager.FindByNameAsync(loginrequest.UserName);

            if (loginrequest.Role == "Student")
            {
                var result = await _mediator.Send(new GetAllCoursesOfStudentQuery { StudentId = user.Id });


                if (result.IsSuccess)
                {
                    authResult.StudentCourses = result.Value;
                }
            }
            else if (loginrequest.Role == "Professor") {
                var result = await _mediator.Send(new GetAllCoursesOfProfessorQuery { ProfessorId = user.Id });


                if (result.IsSuccess)
                {
                    authResult.ProffessorCourses = result.Value;
                }

            }
            return Ok(authResult);
        }

        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken()
        {
            var tmp = Request.Cookies;
            var refreshToken = Request.Cookies["refreshToken"];

            var result = await _jwtTokenGenerator.RefreshTokenAsync(refreshToken);

            if (!result.IsAuthenticated)
                return BadRequest(result);

            SetRefreshTokenInCookie(result.RefreshToken, result.RefreshTokenExpiration);


            return Ok(result);
        }

        [HttpPost("RevokeToken")]
        public async Task<IActionResult> RevokeToken()
        {
            var token = Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(token))
                return BadRequest("Token is required!");

            var result = await _jwtTokenGenerator.RevokeTokenAsync(token);

            if (!result)
                return BadRequest("Token is invalid!");

            return Ok();
        }


        [HttpPost("ForgetPassword")]
        public async Task<IActionResult> ForgetPassword([FromBody] string Email)
        {
            var user = await _userManager.FindByEmailAsync(Email);

            if (user is null)
                return BadRequest("Invalid Email");

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var ResetPasswordLink = Url.Action("ResetPassword", "Authentication", new { token, email = user.Email, Request.Scheme });


            await _mailingService.SendEmailAsync(user.Email, "Reset Password", ResetPasswordLink);

            return Ok();
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPassword resetPassword)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid Input Data");


            var user = await _userManager.FindByEmailAsync(resetPassword.Email);

            if (user is null)
                return NotFound("Invalid Email");

            var resetPassResult = await _userManager.ResetPasswordAsync(user, resetPassword.Token, resetPassword.Password);

            if (!resetPassResult.Succeeded)
                return BadRequest("Invalid Token");



            return Ok("Password Updated Successfully");
        }


        private void SetRefreshTokenInCookie(string refreshToken, DateTime expires)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = expires.ToLocalTime(),
                Secure = false,
                IsEssential = true,
                SameSite = SameSiteMode.None,
                // Domain="localhost"
            };


            Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
            var temp = Response.Cookies;

        }
    }
}
