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
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.Razor;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Api.Pages;
using Application.CQRS.Query.Sections;

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
        private readonly IRazorPartialToStringRenderer _renderer;

        public AuthenticationController(SignInManager<User> signInManager, UserManager<User> userManager
            , IJwtTokenGenerator jwtTokenGenerator, IMailingService mailingService, IMediator mediator
          , IRazorPartialToStringRenderer renderer)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtTokenGenerator = jwtTokenGenerator;
            _mailingService = mailingService;
            _mediator = mediator;
            _renderer = renderer;
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginrequest)
        {
            if (!ModelState.IsValid)
                return Unauthorized("Invalid Email Or Password");

            AuthResult authResult = new AuthResult { };
            try
            {

                authResult = await _jwtTokenGenerator.GenerateToken(loginrequest);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }

            if (authResult.IsAuthenticated)
                SetRefreshTokenInCookie(authResult.RefreshToken, authResult.RefreshTokenExpiration);
            else
                return Unauthorized("Invalid Email Or Password");


            var user = await _userManager.FindByNameAsync(loginrequest.UserName);

            var Roles = await _userManager.GetRolesAsync(user);

            authResult.Roles = Roles;
            authResult.ImageUrl = user.ImageUrl;

            try
            {
                if (Roles[0] == "Student")
                {
                    var result = await _mediator.Send(new GetAllCoursesOfStudentQuery { StudentId = user.Id });


                    if (result.IsSuccess)
                    {
                        authResult.StudentCourses = result.Value;
                    }
                    else return StatusCode(500, "An error occured while trying to retrive the user data");

                }
                else if (Roles[0] == "Professor")
                {
                    var result = await _mediator.Send(new GetAllCoursesOfProfessorQuery { ProfessorId = user.Id });


                    if (result.IsSuccess)
                    {
                        authResult.ProffessorCourses = result.Value;
                    }
                    else return StatusCode(500, "An error occured while trying to retrive the user data");

                }
                else if (Roles[0] == "Instructor")
                {

                    var result = await _mediator.Send(new GetAllSectionsOfInstructorQuery { InstructorId = user.Id });

                    if (result.IsSuccess)
                    {
                        authResult.InstructorSections = result.Value;
                    }
                    else return StatusCode(500, "An error occured while trying to retrive the user data");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }

            return Ok(authResult);
        }

        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken()
        {
            var tmp = Request.Cookies;
            if (tmp is null) { return BadRequest("Invalid Request"); }

            var refreshToken = Request.Cookies["refreshToken"];
            if (refreshToken is null) return BadRequest("Invalid Token");

            AuthResult result = new AuthResult();
            try
            {
                result = await _jwtTokenGenerator.RefreshTokenAsync(refreshToken);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }

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
                return BadRequest("Invalid Token");

            bool result = new bool();
            try
            {
                result = await _jwtTokenGenerator.RevokeTokenAsync(token);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }

            if (!result)
                return BadRequest("Invalid Token");

            return Ok("Token Revoked Successfully");
        }


        [HttpPost("ForgetPassword")]
        public async Task<IActionResult> ForgetPassword([FromBody] string Email)
        {
            var user = await _userManager.FindByEmailAsync(Email);

            if (user is null)
                return BadRequest("Invalid Email");


            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            if (token is null)
            {
                return StatusCode(500, "Internal Server Error");
            }
            var ResetPasswordLink = $"http://localhost:3000/password-reset?token={token}";

            PasswordResetModel model = new PasswordResetModel
            {
                ResetPasswordUrl = ResetPasswordLink,
                Name = user.FirstName + " " + user.SecondName,
            };

            try
            {

                var htmlContent = await _renderer.RenderPartialToStringAsync("EmailResetPasswordTemplate", model);

                await _mailingService.SendEmailAsync(user.Email, "Reset Password", htmlContent);

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
            return Ok("Reset Password Email Sent successfully");
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPassword resetPassword)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid Input Data");


            var user = await _userManager.FindByEmailAsync(resetPassword.Email);

            if (user is null)
                return NotFound("Invalid Email");

            IdentityResult resetPassResult = new IdentityResult();
            try
            {
                resetPassResult = await _userManager.ResetPasswordAsync(user, resetPassword.Token, resetPassword.Password);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }

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
