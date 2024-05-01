using Application.Common.Interfaces.Presistance;
using Contract.Dto.Courses;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.InterfacesForRepository
{
    public interface ICourseRepository :IBaseRepository<Course>
    {
        public Task<AcadimicYear> GetAcadimicYearHasSpecificCourse(int courseId);
        public Task<IEnumerable< CourseLessInfoDto>> GetAllCoursesOfAcadimicYearAndCourseCategory(int AcadimicYearId, int? CourseCategoryId);

        public Task<IEnumerable<CourseOfStudentDto>> GetAllCoursesOfStudent(int StudentId);

        public Task<IEnumerable<CourseOfProfessorDto>> GetAllCoursesOfProfessor (int ProfessorId);

        public Task<IEnumerable<int>> GetAllCoursesIdOfAcadimicYearId(int AcadimicYearId);

        public Task<bool> CheckIfStudentHasAccessToCourse(string StudentUserName, int CourseId);
    }
}
