using Application.Common.Interfaces.CQRSInterfaces;
using Contract.Dto.Assignements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Query.Assignements
{
    public class GetAssignementsResourceByIdQuery : IQuery<AssignemntFilesDto>
    {
       public AssignmentsResourseToAnyUserDto assignmentsResourseToAnyUserDto { get; set; }   
    }
}
