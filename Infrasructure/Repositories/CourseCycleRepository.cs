using Application.Common.Interfaces.InterfacesForRepository;
using Contract.Dto;
using Contract.Dto.CourseCycles;
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

        public async Task<IEnumerable<NameIdDto>> GetAllLessInfoCourseCycles(int courseId, int groupId)
        {
            return await (from cc in _appDbContext.CourseCycles
                          where cc.CourseId == courseId && cc.GroupId == groupId
                          select new NameIdDto
                          {
                              Id = cc.CourseCycleId,
                              Name = cc.Title

                          }).ToListAsync();
        }

        public Task<CourseCycle> GetCourseCycleContainSpecificSectionUsingSectionId(int SectionId)
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetCourseCycleIdOfCourseAndGroup(int courseId, int groupId)
        {
            return await _appDbContext.CourseCycles.Where(cc => cc.GroupId == groupId && cc.CourseId == courseId).Select(cc => cc.CourseCycleId).FirstOrDefaultAsync();
        }

        public async Task<CourseCycle> GetCourseCycleUsingCourseIdAndGroupIdAsync(int courseId, int groupId)
        {
            var courseCycle = await _appDbContext.CourseCycles.Where(cc=>cc.CourseId==courseId && cc.GroupId==groupId).Include(cc=>cc.Course).FirstOrDefaultAsync();
            return courseCycle; 
        }

        public async Task<IEnumerable<CourseCycleWithProfInfoDto>> GetCourseCylcesWithProfInfo(int courseId, int groupId)
        {
            var CourseCyclesWithProfInfo =await (from cc in _appDbContext.CourseCycles
                                            where cc.CourseId == courseId && cc.GroupId == groupId
                                            join user in _appDbContext.Users on cc.ProfessorId equals user.Id
                                            select new CourseCycleWithProfInfoDto
                                            {
                                                CourseCycleId = cc.CourseCycleId,
                                                ProfessorFirstName = user.FirstName,
                                                ProfessorSecondName = user.SecondName,
                                                ProfessorUserName = user.UserName,
                                                ProfessorImageUrl = user.ImageUrl
                                            }).ToListAsync();

            return CourseCyclesWithProfInfo;
        }

        public async Task<bool> CheckIfProfInCourse(int ProfId, int CourseId)
        {

            var CourseCycle = await _appDbContext.CourseCycles.FirstOrDefaultAsync(cc => cc.CourseId == CourseId && cc.ProfessorId == ProfId);
            return CourseCycle != null;
        }
    }
}
