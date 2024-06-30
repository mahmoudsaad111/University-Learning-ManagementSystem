using Application.Common.Interfaces.Presistance;
using Contract.Dto;
using Contract.Dto.Groups;
using Domain.Models;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.InterfacesForRepository
{
    public interface IGroupRepository :IBaseRepository<Group>   
    {
        public Task<AcadimicYear> GetAcadimicYearHasSpecificGroup(int groupId);
        public Task<IEnumerable<GroupLessInfoDto>> GetGroupsOfDepartement(int DepartementId);
        public Task<IEnumerable<NameIdDto>> GetLessInfoGroupsOfAcadimicYear(int AcadimicYearId);
        public Task<IEnumerable<Message>> GetGroupChat(int GroupId); 

    }
}
 