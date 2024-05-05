using Application.Common.Interfaces.InterfacesForRepository;
using Contract.Dto.StudentSection;
using Domain.Models;
using Infrastructure.Common;
using InfraStructure;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class StudentSectionRepository : BaseRepository<StudentSection>, IStudentSectionRepository
    {
        public StudentSectionRepository(AppDbContext _appDbContext) : base(_appDbContext)
        {
        }

        public async Task<IEnumerable<int>> GetAllSectionsIdofStudent(int StudentId)
        {

            var SectionsIdOfStudent = await (from SCC in _appDbContext.StudentsInCourseCycles
                                             where SCC.StudentId == StudentId
                                             join SinS in _appDbContext.StudentSections on SCC.StudentCourseCycleId equals SinS.StudentCourseCycleId
                                             select SinS.SectionId
                                             ).ToListAsync();

            return SectionsIdOfStudent; 
        }

        public async Task<bool> CheckIfStudentInSection(int StudentId, int SectionId)
        {
            var courseCycleIdOfSection = await _appDbContext.Sections.Where(s => s.SectionId == SectionId).Select(s => s.CourseCycleId).FirstOrDefaultAsync();
            if(courseCycleIdOfSection==0)
                return false;

            var StudentSectionId = await (from SCC in _appDbContext.StudentsInCourseCycles
                                 where SCC.StudentId == StudentId && SCC.CourseCycleId==courseCycleIdOfSection
                                 from SinS in _appDbContext.StudentSections 
                                 where SinS.SectionId == SectionId && SCC.StudentCourseCycleId == SinS.StudentCourseCycleId
                                 select  SinS.StudentSectionId
                                 ).FirstOrDefaultAsync();

            return (StudentSectionId != 0) ;
        }

        public async Task<int> GetStudentSectionId(int SectionId, int StudentCourseCylceId)
        {
            return await _appDbContext.StudentSections.Where(ss => ss.SectionId == SectionId && ss.StudentCourseCycleId == StudentCourseCylceId).Select(ss=>ss.StudentSectionId).FirstOrDefaultAsync();

        }

        public async Task<IEnumerable<int>> GetAllStudentsIdOnSection(int SectionId)
        {
            return await (from SinS in _appDbContext.StudentSections
                          where SinS.SectionId == SectionId
                          join Scc in _appDbContext.StudentsInCourseCycles on SinS.StudentCourseCycleId equals Scc.StudentCourseCycleId
                          select Scc.StudentId).ToListAsync();
        }

        public async Task<bool> CheckIfStudnetInSectionByUserName(string StudentUserName, int SectionId)
        {
            int StudentId= await _appDbContext.Users.AsNoTracking().Where(u=>u.UserName==StudentUserName).
                Select(u=>u.Id)
                .FirstOrDefaultAsync();   
            
            if(StudentId== 0)
                return false;
            return await this.CheckIfStudentInSection(StudentId, SectionId);
        }
    }
}
