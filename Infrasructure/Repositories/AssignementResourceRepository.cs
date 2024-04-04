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
    public class AssignementResourceRepository : BaseRepository<AssignmentResource>, IAssignementResourceRepository
    {
        public AssignementResourceRepository(AppDbContext _appDbContext) : base(_appDbContext)
        {
        }

        public async Task<IEnumerable<string>> GetAllFilesUrlForAssignementAsync(int AssignementId)
        {
            try
            {
                return await _appDbContext.AssignmentResources.Where(ar => ar.AssignmentId == AssignementId).Select(ar => ar.Url).ToListAsync();
            }
            catch (Exception ex)
            {
                return new List<string>();
            }
        }
    }
}
