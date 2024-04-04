using Application.Common.Interfaces.Presistance;
using Domain.Models;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.InterfacesForRepository
{
    public interface IGroupRepository :IBaseRepository<Group>   
    {
        public Task<AcadimicYear> GetAcadimicYearHasSpecificGroup(int groupId);
    }
}
 