using Application.Common.Interfaces.Presistance;
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
    }
}
