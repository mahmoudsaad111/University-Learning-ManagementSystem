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
    public class CourseCycleRepository : BaseRepository<CourseCycle>, ICourseCycle
    {
        public CourseCycleRepository(AppDbContext _appDbContext) : base(_appDbContext)
        {
        }

        public Task<CourseCycle> GetCourseCycleContainSpecificSectionUsingSectionId(int SectionId)
        {
            throw new NotImplementedException();
        }

        public async Task<CourseCycle> GetCourseCycleUsingCourseIdAndGroupIdAsync(int courseId, int groupId)
        {
            var courseCycle = await _appDbContext.CourseCycles.Where(cc=>cc.CourseId==courseId && cc.GroupId==groupId).Include(cc=>cc.Course).FirstOrDefaultAsync();
            return courseCycle; 
        }
    }
}
