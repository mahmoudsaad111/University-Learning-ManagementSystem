using Application.Common.Interfaces.InterfacesForRepository;
using Domain.Models;
using Infrastructure.Common;
using InfraStructure;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repositories
{
    public class AssignementAnswerResourceRepository : BaseRepository<AssignmentAnswerResource>, IAssignementAnswerResourceRepository
    {
        public AssignementAnswerResourceRepository(AppDbContext _appDbContext) : base(_appDbContext)
        {
        }

        public async Task<IEnumerable<AssignmentAnswerResource>> GetAllFilesUrlForAssignementAnswerAsync(int AssignementAnswerId)
        {
            try
            {
                return await _appDbContext.AssignmentAnswerResources.AsNoTracking().Where(ar => ar.AssignmentAnswerId == AssignementAnswerId).ToListAsync();
            }
            catch (Exception ex)
            {
                return new List<AssignmentAnswerResource>();
            }
        }
    }
}
