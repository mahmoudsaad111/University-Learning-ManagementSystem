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
    public class StudentSectionRepository : BaseRepository<StudentSection>, IStudentSectionRepository
    {
        public StudentSectionRepository(AppDbContext _appDbContext) : base(_appDbContext)
        {
        }

        public async Task<IEnumerable<int>> GetAllSectionsIdofStudent(int StudentId)
        {
            return await _appDbContext.StudentSections.AsNoTracking().Where(ss => ss.StudentId == StudentId).Select(ss => ss.SectionId).ToListAsync();
        }
    }
}
