using Application.Common.Interfaces.Presistance;
using Contract.Dto;
using Contract.Dto.CourseCycles;
using Contract.Dto.Courses;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.InterfacesForRepository
{
    public interface ICourseCycle  : IBaseRepository<CourseCycle>
    {
        public Task<CourseCycle> GetCourseCycleUsingCourseIdAndGroupIdAsync(int courseId, int groupId);
        public Task<CourseCycle> GetCourseCycleContainSpecificSectionUsingSectionId(int SectionId);
        public Task<int> GetCourseCycleIdOfCourseAndGroup(int courseId, int groupId);
        public Task<IEnumerable<CourseCycleWithProfInfoDto>> GetCourseCylcesWithProfInfo(int courseId, int groupId);

        public Task<IEnumerable<NameIdDto>> GetAllLessInfoCourseCycles(int courseId, int groupId);
        public Task<bool> CheckIfProfInCourse(int ProfId, int CourseId); 
    }
}
