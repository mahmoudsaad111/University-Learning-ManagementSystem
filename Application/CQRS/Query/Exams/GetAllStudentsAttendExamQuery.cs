using Application.Common.Interfaces.CQRSInterfaces;
using Contract.Dto.StudentExamDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Query.Exams
{
    public class GetAllStudentsAttendExamQuery :IQuery<IEnumerable<StudentAttendExamDto>>
    {
        public string ExamCreatorUserName { get; set; } 
        public int ExamId {  get; set; }    
    }
}
