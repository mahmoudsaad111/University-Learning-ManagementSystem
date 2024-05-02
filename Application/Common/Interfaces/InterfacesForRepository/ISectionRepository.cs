using Application.Common.Interfaces.Presistance;
using Contract.Dto.Sections;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace Application.Common.Interfaces.InterfacesForRepository
{
    public interface ISectionRepository :IBaseRepository<Section>   
    {
        public Task<Section> GetSectionHasSpecificLectureUsingLectureIdAsync(int LectureId);
        public Task<IEnumerable<int>> GetAllSectionsIdOfInstructore(int instructorId);
        public Task<IEnumerable<int>> GetAllSectionsIdOfProfessor(int ProfesssorId);

        public Task<bool> CheckIfInstructorInSection(int InstrucotrId, int SectionId); 

        public Task<SectionsOfCoursesToStudentDto> GetSectionOfCoursesToStudent (int StudentId , int CourseCycleId);

        public Task<IEnumerable<SectionOfInstructorDto>> GetSectionsOfInstructor(int InstructorId);

        public Task<IEnumerable<SectionOfCourseCycleDto>> GetSectionsOfCourseCycle(int CourseCycelId);

        public Task<IEnumerable<SectionsOfProfessorDto>> GetAllSectionsOfProfessor(int  ProfesssorId);

        public Task<bool> CheckIfInstructorInCourse(int InstructorId, int CourseId);
    }
}
