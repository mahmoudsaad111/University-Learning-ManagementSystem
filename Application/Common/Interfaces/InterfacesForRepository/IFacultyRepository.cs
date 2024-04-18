using Application.Common.Interfaces.Presistance;
using Contract.Dto;
using Domain.Models;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.InterfacesForRepository
{
    public interface IFacultyRepository :IBaseRepository<Faculty>
    {
        public Task<Result<Faculty>> GetFacultyHasThisDepartementAsync(int deptId);
        public Task<IEnumerable<NameIdDto>> GetLessInfoFaculties();
    }
}
