using Application.Common.Interfaces.Presistance;
using Contract.Dto.CourseCategories;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.InterfacesForRepository
{
    public interface ICourseCategoryRepository :IBaseRepository<CourseCategory>
    {
        public Task<IEnumerable<CourseCategoryWithDeptAndFacultyDto>> GetAllCourseCategoriesWithDeptAndFaculty();
    }
}
