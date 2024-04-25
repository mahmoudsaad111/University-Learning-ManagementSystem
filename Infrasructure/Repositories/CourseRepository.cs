using Application.Common.Interfaces.InterfacesForRepository;
using Contract.Dto.Courses;
using Domain.Models;
using Infrastructure.Common;
using InfraStructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
	public class CourseRepository : BaseRepository<Course> , ICourseRepository
	{
		public CourseRepository(AppDbContext _appDbContext) : base(_appDbContext)
		{
		}

        public async Task<AcadimicYear> GetAcadimicYearHasSpecificCourse(int courseId)
        {
            try
            {
                var Course = await _appDbContext.Courses.AsNoTracking().Include(c=>c.AcadimicYear).FirstOrDefaultAsync(c=>c.CourseId == courseId);
                if (Course == null)
                    return null; 
                return Course.AcadimicYear;
            }
            catch (Exception ex)
            {
                return null; 
            }
        }

        public async Task<IEnumerable<int>> GetAllCoursesIdOfAcadimicYearId(int AcadimicYearId)
        {
            return await _appDbContext.Courses.AsNoTracking().Where(c=>c.AcadimicYearId==AcadimicYearId).Select(c=>c.CourseId).ToListAsync();
        }

        public async Task<IEnumerable<CourseLessInfoDto>> GetAllCoursesOfAcadimicYearAndCourseCategory(int AcadimicYearId, int? CourseCategoryId)
        {
            IEnumerable<CourseLessInfoDto> courseLessInfoDtos = null;
            if (CourseCategoryId is not null) 
            {
                courseLessInfoDtos= await _appDbContext.Courses.AsNoTracking().Where(c => c.CourseCategoryId == CourseCategoryId && c.AcadimicYearId == AcadimicYearId).Select(Cl => new CourseLessInfoDto
                {
                    CourseId = Cl.CourseId,
                    Name = Cl.Name,
                    Description = Cl.Description,
                    TotalMark = Cl.TotalMark
                }).ToListAsync();                                    
            }

            else
            {
                courseLessInfoDtos = await _appDbContext.Courses.AsNoTracking().Where(c=>c.AcadimicYearId == AcadimicYearId).Select(Cl => new CourseLessInfoDto
                {
                    CourseId = Cl.CourseId,
                    Name = Cl.Name,
                    Description = Cl.Description,
                    TotalMark = Cl.TotalMark
                }).ToListAsync();
            }

            return courseLessInfoDtos;
        }

        public async Task<IEnumerable<CourseOfProfessorDto>> GetAllCoursesOfProfessor(int ProfessorId)
        {
           var CourseOfProfessor = await (from courseCycle in _appDbContext.CourseCycles
                                   where courseCycle.ProfessorId == ProfessorId
                                   join _group in _appDbContext.Groups on courseCycle.GroupId equals _group.GroupId
                                   join acadimicYear in _appDbContext.AcadimicYears on _group.AcadimicYearId equals acadimicYear.AcadimicYearId
                                   join course in _appDbContext.Courses on courseCycle.CourseId equals course.CourseId
                                   where course.AcadimicYearId == _group.AcadimicYearId

                                   join departement in _appDbContext.Departements on acadimicYear.DepartementId equals departement.DepartementId
                                   join faculty in _appDbContext.Faculties on departement.FacultyId equals faculty.FacultyId

                                   select new CourseOfProfessorDto
                                   {
                                       CourseCycleId= courseCycle.CourseId,
                                       GroupId=_group.GroupId ,
                                       GroupName=_group.Name,                               
                                       CourseId = course.CourseId,
                                       CourseName = course.Name,
                                       AcadimicYearId=acadimicYear.AcadimicYearId ,
                                       Year=acadimicYear.Year ,
                                       DepartementId=departement.DepartementId,
                                       DepartmentName=departement.Name,
                                       FacultyId=faculty.FacultyId,
                                       FacultyName=faculty.Name

                                   }).ToListAsync();

            return CourseOfProfessor;

        }

        public  async Task<IEnumerable<CourseOfStudentDto>> GetAllCoursesOfStudent(int StudentId)
        {
            var CurrentStudent =await _appDbContext.Students.FirstOrDefaultAsync(s=>s.StudentId == StudentId);

            if (CurrentStudent is not null)
            {
                int GroupId = CurrentStudent.GroupId;

                var CoursesOfStudent = await (from Group in _appDbContext.Groups
                                        where GroupId == Group.GroupId
                                        join CC in _appDbContext.CourseCycles on Group.GroupId equals CC.GroupId
                                        join SCC in _appDbContext.StudentsInCourseCycles on CC.CourseCycleId equals SCC.CourseCycleId
                                        where SCC.StudentId == StudentId
                                        join Course in _appDbContext.Courses on CC.CourseId equals Course.CourseId
                                        join Professor in _appDbContext.Professors on CC.ProfessorId equals Professor.ProfessorId
                                        join User in _appDbContext.Users on Professor.ProfessorId equals User.Id
                                        select new CourseOfStudentDto
                                        {
                                            GroupId = Group.GroupId,
                                            GroupName = Group.Name,
                                            CourseId = Course.CourseId,
                                            CourseName = Course.Name,
                                            TotalMarksOfStudent=SCC.MarksOfStudent,
                                            ProfessorId = Professor.ProfessorId,
                                            ProfessorFirstName = User.FirstName,
                                            ProfessorSecondName = User.SecondName,
                                            ProfessorUserName = User.UserName,
                                            ProfessorImageUrl= User.ImageUrl
                                        }).ToListAsync();
                return CoursesOfStudent;
            }
            return Enumerable.Empty<CourseOfStudentDto>();
        }
    }
}
