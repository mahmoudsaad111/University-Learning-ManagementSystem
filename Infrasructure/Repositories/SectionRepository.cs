using Application.Common.Interfaces.InterfacesForRepository;
using Contract.Dto.ReturnedDtos;
using Contract.Dto.Sections;
using Domain.Models;
using Infrastructure.Common;
using InfraStructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
 
namespace Infrastructure.Repositories
{
    public class SectionRepository : BaseRepository<Section>, ISectionRepository
	{
		public SectionRepository(AppDbContext _appDbContext) : base(_appDbContext)
		{
		}

        public async Task<IEnumerable<int>> GetAllSectionsIdOfInstructore(int instructorId)
        {
            return await _appDbContext.Sections.AsNoTracking().Where(s => s.InstructorId == instructorId).Select(s => s.SectionId).ToListAsync();
        }

        public async Task<IEnumerable<int>> GetAllSectionsIdOfProfessor(int ProfesssorId)
        {
            return await (
                            from P in _appDbContext.Professors
                            join CC in _appDbContext.CourseCycles on P.ProfessorId equals CC.ProfessorId
                            join S in _appDbContext.Sections on CC.CourseCycleId equals S.CourseCycleId
                            select S.SectionId
                            
                          ).ToListAsync();                           
        }

        public async Task<Domain.Models.Section> GetSectionHasSpecificLectureUsingLectureIdAsync(int LectureId)
        {
                var TargetSection = await _appDbContext.Sections.FirstOrDefaultAsync(s => s.Lectures.Any(l => l.LectureId == LectureId));
                return TargetSection; 
        }

  
        public async Task<SectionsOfCoursesToStudentDto> GetSectionOfCoursesToStudent(int StudentId, int CourseCycleId)
        {


            var SectionOfCourse = await ( 
                                         from SCC in _appDbContext.StudentsInCourseCycles
                                         where SCC.StudentId == StudentId && SCC.CourseCycleId== CourseCycleId
                                         join SinS in _appDbContext.StudentSections on SCC.StudentCourseCycleId equals SinS.StudentCourseCycleId
                                         join section in _appDbContext.Sections on SinS.SectionId equals section.SectionId                
                                         join Instructor in _appDbContext.Instructors on section.InstructorId equals Instructor.InstructorId
                                         join user in _appDbContext.Users on Instructor.InstructorId equals user.Id
                                         select new SectionsOfCoursesToStudentDto
                                         {
                                             SectionId = section.SectionId,
                                             SectionName = section.Name,
                                             SectionDescreption = section.Description,
                                             TotalMarksOfStudent=SinS.StudentTotalMarks,
                                             StudentSectionId=SinS.StudentSectionId,
                                             InstructorId = section.InstructorId,
                                             InstructorNameFirst = user.FirstName,
                                             InstructorSecondName = user.SecondName,
                                             InstructorUserName = user.UserName,
                                             InstructorImageUrl = user.ImageUrl
                                         }).ToListAsync();
            ;

            return SectionOfCourse.FirstOrDefault();
        }

        public async Task<bool> CheckIfInstructorInSection(int InstrucotrId, int SectionId)
        {
            var TargetSection = await _appDbContext.Sections.FirstOrDefaultAsync(sec => sec.SectionId == SectionId);
            return (TargetSection is not null && TargetSection.InstructorId == InstrucotrId);
        }
        public async Task<bool> CheckIfProfessorInSection(int ProfessorId, int SectionId)
        {
            var CorrectProffId = await (
                                               from s in _appDbContext.Sections where s.SectionId == SectionId
                                               from cc in _appDbContext.CourseCycles where s.CourseCycleId == s.CourseCycleId
                                               select cc.ProfessorId                            
                       ).FirstOrDefaultAsync();

            return (CorrectProffId != 0 && ProfessorId == CorrectProffId);
        }
        public async Task<IEnumerable<SectionOfInstructorDto>> GetSectionsOfInstructor(int InstructorId)
        {
            var SectionsOfInstructor = await (
                                                from Instructor in _appDbContext.Instructors
                                                where Instructor.InstructorId == InstructorId

                                                join section in _appDbContext.Sections on Instructor.InstructorId equals section.InstructorId
                                                join courseCycle in _appDbContext.CourseCycles on section.CourseCycleId equals courseCycle.CourseCycleId

                                                join course in _appDbContext.Courses on courseCycle.CourseId equals course.CourseId

                                                join _group in _appDbContext.Groups on courseCycle.GroupId equals _group.GroupId
                                                join acadimicYear in _appDbContext.AcadimicYears on _group.AcadimicYearId equals acadimicYear.AcadimicYearId

                                                join professor in _appDbContext.Professors on courseCycle.ProfessorId equals professor.ProfessorId
                                                join user in _appDbContext.Users on professor.ProfessorId equals user.Id

                                                select new SectionOfInstructorDto
                                                {
                                                    SectionId = section.SectionId,
                                                    SectionName = section.Name,
                                                    SectionDescreption = section.Description,
                                                    ProfessorFirstName = user.FirstName,
                                                    ProfessorSecondName = user.SecondName,
                                                    ProfessorUserName = user.UserName,
                                                    CourseId = course.CourseId,
                                                    CourseName = course.Name,
                                                    GroupId = _group.GroupId,
                                                    GroupName = _group.Name,
                                                    AcadimicYearId = acadimicYear.AcadimicYearId,
                                                    Year = acadimicYear.Year,
                                                    CourseCycleId=courseCycle.CourseCycleId    
                                                }

                                              ).ToListAsync();

            return SectionsOfInstructor;
        }

        public async Task<IEnumerable<SectionOfCourseCycleDto>> GetSectionsOfCourseCycle(int CourseCycelId)
        {
            var SectionsOfCourseCycle = await (from courseCycle in _appDbContext.CourseCycles
                                               where courseCycle.CourseCycleId == CourseCycelId
                                               join section in _appDbContext.Sections on courseCycle.CourseCycleId equals section.CourseCycleId
                                             //  join instructor in _appDbContext.Instructors on section.InstructorId equals instructor.InstructorId
                                               join user in _appDbContext.Users on section.InstructorId equals user.Id
                                               select new SectionOfCourseCycleDto
                                               {
                                                   SectionId = section.SectionId,
                                                   SectionName = section.Name,
                                                   InstructorFirstName = user.FirstName,
                                                   InstructorSecondName = user.SecondName,
                                                   InstructorUrlImage = user.ImageUrl,
                                                   InsturctorUserName = user.UserName
                                               }
                                        ).ToListAsync();


            return SectionsOfCourseCycle; 

        }

        public async Task<IEnumerable<SectionsOfProfessorDto>> GetAllSectionsOfProfessor(int ProfesssorId)
        {
            var SectionsOfProfessor = await (from courseCycel in _appDbContext.CourseCycles
                                       where courseCycel.ProfessorId == ProfesssorId
                                       join section in _appDbContext.Sections on courseCycel.CourseCycleId equals section.CourseCycleId
                                       join course in _appDbContext.Courses on courseCycel.CourseId equals course.CourseId
                                       join _group in _appDbContext.Groups on courseCycel.GroupId equals _group.GroupId
                                       where _group.AcadimicYearId == course.AcadimicYearId
                                       join acadimicYear in _appDbContext.AcadimicYears on course.AcadimicYearId equals acadimicYear.AcadimicYearId
                                       join departement in _appDbContext.Departements on acadimicYear.DepartementId equals departement.DepartementId
                                       join faculty in _appDbContext.Faculties on departement.FacultyId equals faculty.FacultyId
                                       join user in _appDbContext.Users on section.InstructorId equals user.Id

                                       select new SectionsOfProfessorDto
                                       {
                                           CourseCycleId = courseCycel.CourseCycleId,
                                           CourseId = course.CourseId,
                                           CourseName = course.Name,
                                           GroupId = _group.GroupId,
                                           GroupName = _group.Name,

                                           SectionId = section.SectionId,
                                           SectionName = section.Name,
                                           SectionDescreption = section.Description,

                                           InstructorFirstName = user.FirstName,
                                           InstructorSecondName = user.SecondName,
                                           InstructorImageUrl = user.ImageUrl,
                                           InstructorUserName = user.UserName,

                                           AcadimicYearId = acadimicYear.AcadimicYearId,
                                           Year = acadimicYear.Year,

                                           DepartementId = departement.DepartementId,
                                           DepartementName = departement.Name,

                                           FacultyId = faculty.FacultyId,
                                           FacultyName = faculty.Name
                                       }
                                      ).ToListAsync();

            return SectionsOfProfessor;
        }

        public async Task<SectionAndCourseCycleNameDto> GetSectionAndCourseCycleName(int SectionId) {
            var SectionAndCourseCycleNames = await (from s in _appDbContext.Sections
                                                    where s.SectionId == SectionId
                                                    from sc in _appDbContext.CourseCycles
                                                    where s.CourseCycleId==sc.CourseCycleId
                                                    select new SectionAndCourseCycleNameDto
                                                    {
                                                        SectionName=s.Name,
                                                        CourseCycleName=sc.Title
                                                    }
                                                    ).FirstOrDefaultAsync();

            return SectionAndCourseCycleNames; 
        }
    }
}
