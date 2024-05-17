using Application.Common.Interfaces.CQRSInterfaces;
using Contract.Dto.Assignements;
using Domain.Enums;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Query.Assignements
{
    public class GetAllAssignementsOfSectionQuery : IQuery<IEnumerable<Assignment>>
    {
       public AssignmentOfSectionToAnyUserDto assignmentToAnyUserDto { get; set; }
    }
}
