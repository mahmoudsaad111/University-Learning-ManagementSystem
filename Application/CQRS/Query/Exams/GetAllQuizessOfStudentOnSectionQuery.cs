using Application.Common.Interfaces.CQRSInterfaces;
using Contract.Dto.Exams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Query.Exams
{
    public class GetAllQuizessOfStudentOnSectionQuery : IQuery<IEnumerable<ExamOfStudentToDto>>
    {
        public int SectionId { get; set; }
        public string StudentUserName { get; set; }    
    }
}
