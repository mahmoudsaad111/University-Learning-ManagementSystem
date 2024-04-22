using Application.Common.Interfaces.CQRSInterfaces;
using Contract.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Query.Groups
{
    public class GetAllGroupsOfAcadimicYearQuery : IQuery<IEnumerable<NameIdDto>>
    {
        public int AcadimicYearId { get; set; }   
    }
}
