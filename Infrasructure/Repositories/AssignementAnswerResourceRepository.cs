using Application.Common.Interfaces.InterfacesForRepository;
using Domain.Models;
using Infrastructure.Common;
using InfraStructure;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repositories
{
    public class AssignementAnswerResourceRepository : BaseRepository<AssignmentAnswerResource>, IAssignementAnswerResouceRepository
    {
        public AssignementAnswerResourceRepository(AppDbContext _appDbContext) : base(_appDbContext)
        {
        }

        public async Task<IEnumerable<string>> GetAllFilesUrlForAssignementAnswerAsync(int AssignementAnswerId)
        {
            try
            {
                return await _appDbContext.AssignmentAnswerResources.Where(ar => ar.AssignmentAnswerId == AssignementAnswerId).Select(ar => ar.Url).ToListAsync();
            }
            catch (Exception ex)
            {
                return new List<string>();
            }
        }
    }
}
