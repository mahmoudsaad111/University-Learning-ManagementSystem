using Application.Common.Interfaces.CQRSInterfaces;
using Contract.Dto.Assignements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Query.Assignements
{
    public class GetEmailsOfStudnetsHavingAccessToAssignmentQuery : IQuery<IEnumerable<string>>
    {
        public int AssignmentId { get; set; }   
    }
}
