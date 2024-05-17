using Application.Common.Interfaces.CQRSInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Query.StudentExams
{
    public class GetEmailsOfStudentsHavingAccessToExamQuery :IQuery<IEnumerable<string>>
    {
        public int ExamId { get; set; } 
    }
}
