using Application.Common.Interfaces.Presistance;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.InterfacesForRepository
{
    public interface IAssignementAnswerResouceRepository :IBaseRepository<AssignmentAnswerResource>
    {
        Task<IEnumerable<string>> GetAllFilesUrlForAssignementAnswerAsync(int AssignementAnswerId);
    }
}
