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
    }
}
