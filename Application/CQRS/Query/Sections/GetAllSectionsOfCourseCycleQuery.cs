using Application.Common.Interfaces.CQRSInterfaces;
using Contract.Dto.Sections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Query.Sections
{
    public class GetAllSectionsOfCourseCycleQuery : IQuery<IEnumerable<SectionOfCourseCycleDto>>
    {
        public int CourseCycleId { get; set; }  
    }
}
