using Application.Common.Interfaces.CQRSInterfaces;
using Contract.Dto.Groups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Query.Groups
{
    public class GetAllGroupsOfDepartementQuery : IQuery<IEnumerable<GroupLessInfoDto>>
    {
        public int DepartementId {  get; set; } 
    }
}
