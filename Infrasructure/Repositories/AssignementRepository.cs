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
    public class AssignementRepository : BaseRepository<Assignment>  , IAssignementRepository
    {
        public AssignementRepository(AppDbContext _appDbContext) : base(_appDbContext)
        {
        }

        public async Task<IEnumerable<Assignment>> GetAllAssignementsOfSection(int sectionId)
        {
            return await _appDbContext.Assignments.AsNoTracking().Where(a => a.SectionId == sectionId).ToListAsync();
        }

        public async Task<IEnumerable<string>> GetEmailsOfStudnetsHavingAccessToAssignment(int AssignmentId)
        {
            var EmailsOfStudents = await (from ass in _appDbContext.Assignments
                                          where ass.AssignmentId == AssignmentId
                                          join sec in _appDbContext.Sections on ass.SectionId equals sec.SectionId
                                          join studentSection in _appDbContext.StudentSections on sec.SectionId equals studentSection.SectionId
                                          join studentCourseCycle in _appDbContext.StudentsInCourseCycles on studentSection.StudentCourseCycleId equals studentCourseCycle.StudentCourseCycleId
                                          join user in _appDbContext.Users on studentCourseCycle.StudentId equals user.Id
                                          select user.Email
                                          ).ToListAsync();
            return EmailsOfStudents;
        }
    }
}
