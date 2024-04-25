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
            var StudentSection = await (from SCC in _appDbContext.StudentsInCourseCycles
                                 where SCC.StudentId == StudentId
                                 join SinS in _appDbContext.StudentSections on SCC.StudentCourseCycleId equals SinS.StudentCourseCycleId
                                 where SinS.SectionId == SectionId 
                                 select new StudentSectionDto
                                 {
                                     SectionId = SinS.SectionId,
                                     StudentCourseCycleId=SCC.StudentCourseCycleId,
                                     TotalMarks=SinS.StudentTotalMarks
                                 }

                                 ).FirstOrDefaultAsync();

            return StudentSection is null ? false : true;
        }

        public async Task<int> GetStudentSectionId(int SectionId, int StudentCourseCylceId)
        {
            return await _appDbContext.StudentSections.Where(ss => ss.SectionId == SectionId && ss.StudentCourseCycleId == StudentCourseCylceId).Select(ss=>ss.StudentSectionId).FirstOrDefaultAsync();

        }
    }
}
