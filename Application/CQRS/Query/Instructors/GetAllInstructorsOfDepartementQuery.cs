using Application.Common.Interfaces.CQRSInterfaces;
using Contract.Dto.ReturnedDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Query.Instructors
{
    public class GetAllInstructorsOfDepartementQuery :IQuery<IEnumerable<ReturnedInstructorDto>>
    {
        public int DepartementId { get; set; }  
    }
}
