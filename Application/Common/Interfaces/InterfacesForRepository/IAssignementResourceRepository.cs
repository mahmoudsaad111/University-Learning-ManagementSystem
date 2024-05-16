using Application.Common.Interfaces.Presistance;
using Contract.Dto.Assignements;
using Domain.Models;
using Domain.TmpFilesProcessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.InterfacesForRepository
{
    public interface IAssignementResourceRepository :IBaseRepository<AssignmentResource>
    {
        Task<IEnumerable<AssignmentResource>> GetAllFilesUrlForAssignementAsync(int AssignementId);
        public Task<AssignemntFilesDto> GetFilesOfAssignemnt(int assignemntId);
    }
}
