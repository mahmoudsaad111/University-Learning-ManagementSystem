using Application.Common.Interfaces.InterfacesForRepository;
using Contract.Dto.Assignements;
using Domain.Models;
using Domain.TmpFilesProcessing;
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

        public async Task<IEnumerable<AssignmentResource>> GetAllFilesUrlForAssignementAsync(int AssignementId)
        {
            try
            {
                return await _appDbContext.AssignmentResources.AsNoTracking().Where(ar => ar.AssignmentId == AssignementId).ToListAsync();
            }
            catch (Exception ex)
            {
                return new List<AssignmentResource>();
            }
        }


        public async Task<AssignemntFilesDto> GetFilesOfAssignemnt(int assignemntId)
        {
            var Result = await (from a in _appDbContext.Assignments
                                where a.AssignmentId == assignemntId
                                select new AssignemntFilesDto
                                {
                                    Name = a.Name,
                                    Description = a.Description,
                                    FullMark = a.FullMark,
                                    ResourcesOfAssigenments = (from asr in _appDbContext.AssignmentResources
                                                               where asr.AssignmentId == a.AssignmentId
                                                               select new AssignmentResource
                                                               {
                                                                   Name = asr.Name,
                                                                   Url = asr.Url,
                                                                   FileType = asr.FileType,

                                                               }
                                                               ).ToList()

                                }
                                ).FirstOrDefaultAsync();
            return Result;
        }
    }
}
