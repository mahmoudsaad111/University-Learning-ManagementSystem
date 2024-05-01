using Application.Common.Interfaces.CQRSInterfaces;
using Contract.Dto.StudentExamDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Query.StudentExams
{
    public class GetExamAnswerOfStudentQuery :IQuery<StudentAnswersOfExamDto>
    {
        public string StudentUserName {  get; set; }    
        public int ExamId { get; set; } 
    }
}
