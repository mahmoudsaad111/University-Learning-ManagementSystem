using Application.Common.Interfaces.Presistance;
using Contract.Dto.ReturnedDtos;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.InterfacesForRepository
{
    public interface IInstructorRepository : IBaseRepository<Instructor>
    {
        public Task<IEnumerable<ReturnedInstructorDto>> GetAllInstructorInDepartement(  int DeptId);

    }
}
