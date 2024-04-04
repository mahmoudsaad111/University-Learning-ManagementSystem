using Application.Common.Interfaces.InterfacesForRepository;
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
    }
}
