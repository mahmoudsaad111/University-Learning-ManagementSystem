using Application.Common.Interfaces.InterfacesForRepository;
using Application.Common.Interfaces.Presistance;
using Application.CQRS.Command.StudentCourseCycles;
using Contract.Dto.StudentCourseCycles;
using Domain.Models;
using Infrastructure.Common;
using InfraStructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class StudentCourseCycleRepository : BaseRepository<StudentCourseCycle>, IStudentCourseCycleRepository
    {
        public StudentCourseCycleRepository(AppDbContext _appDbContext) : base(_appDbContext)
        {
        }

        private async Task<StudentCourseCycle> GetStudentCourseCycle(int StudentId, int CourseId, int GroupId)
        {
            int courseCylceId = await _appDbContext.CourseCycles.Where(cc => cc.CourseId == CourseId && cc.GroupId == GroupId).Select(cc => cc.CourseCycleId).FirstOrDefaultAsync();

            return new StudentCourseCycle
            {
                StudentId = StudentId,
                CourseCycleId = courseCylceId
            };
        }
        public async Task<bool> AddStudentToHisCourseCycles(int StudentId  , int AcadimicYearId, int GroupId)
        {
            

            IEnumerable<int> CoursesIdOfAcadimicYearOfStudent = await _appDbContext.Courses.Where(c=>c.AcadimicYearId==AcadimicYearId).Select(c=>c.CourseId).ToListAsync();
            bool Done = true;
            foreach (var courseId in CoursesIdOfAcadimicYearOfStudent)
            {
                var studentCourseCycle = await GetStudentCourseCycle(StudentId, courseId, GroupId);

                if (studentCourseCycle is null || studentCourseCycle.CourseCycleId == 0)
                    continue;

                var Added = await _appDbContext.StudentsInCourseCycles.AddAsync(studentCourseCycle);
                if (Added is null)
                {
                    Done = false;
                    break;
                }
            }
            return Done; 
        }

 
        public async Task<bool> DeleteStudentFromHisCourseCylces(int studentId)
        {
            try
            {
                var StudentCourseCycles = await _appDbContext.StudentsInCourseCycles.Where(scc => scc.StudentId == studentId).ToListAsync();
                _appDbContext.StudentsInCourseCycles.RemoveRange(StudentCourseCycles);
                return true; 
            }
            catch (Exception ex)
            {
                return false; 
            }
        }

        public async Task<int> GetStudentCourseCycleId(int studentId, int courseCycleId)
        {
            return await _appDbContext.StudentsInCourseCycles.Where(scc => scc.StudentId == studentId && scc.CourseCycleId == courseCycleId).Select(scc=>scc.StudentCourseCycleId).FirstOrDefaultAsync();
        }

        public async Task<bool> CheckIfStudnetInCourseCycle(string StudnetUserName, int CourseCylceId)
        {
            int StudentId = await _appDbContext.Users.AsNoTracking().Where(u=>u.UserName==StudnetUserName)
                                                                    .Select(u=>u.Id).FirstOrDefaultAsync();

            if (StudentId == 0)
                return false;
            int StudentCourseCylceId = await this.GetStudentCourseCycleId(StudentId , CourseCylceId);

            if(StudentCourseCylceId == 0) 
                return false;
            return true;

        }
        public async Task<bool> ChekcIfStudentInCourseCycle(int StudentId, int CourseCycleId)
        {
            var StudentCourseCycle = await _appDbContext.StudentsInCourseCycles.FirstOrDefaultAsync(scc => scc.StudentId == StudentId && scc.CourseCycleId == CourseCycleId);

            return StudentCourseCycle != null;
        }
    }
}
