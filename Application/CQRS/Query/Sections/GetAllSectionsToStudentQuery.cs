using Application.Common.Interfaces.CQRSInterfaces;
using Contract.Dto.Sections;
using Contract.Dto.StudentSection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Query.Sections
{
    public class GetAllSectionsToStudentQuery : IQuery<IEnumerable<SectionOfStudentDto>>
    {
        public string StudentUserName { get; set; } 
    }
}
