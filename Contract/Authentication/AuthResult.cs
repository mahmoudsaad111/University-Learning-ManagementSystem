using Contract.Dto.Courses;
using Contract.Dto.Sections;
using Domain.Models;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Authentication
{
    public class AuthResult
    {
        public string JwtToken { get; set; }
        public string RefreshToken { get; set; }

        public string? UserName { get; set; }

        public string? ImageUrl { get; set; }
        public int UserId { get; set; }
        public string Email { get; set; }

        public ICollection<string>? Roles { get; set; }
        public bool IsAuthenticated { get; set; }
        public string? Message { get; set; }
        public DateTime RefreshTokenExpiration { get; set; }

        public DateTime? ExpiresAt { get; set; }

        public IEnumerable<CourseOfStudentDto>? StudentCourses { get; set; }
        public IEnumerable<CourseOfProfessorDto>? ProffessorCourses { get; set; }

        public IEnumerable<SectionOfInstructorDto>? InstructorSections { get; set; }
    }
}
