using Application.Common.Interfaces.CQRSInterfaces;
using Contract.Dto.Exams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Query.Exams
{
    public class GetAllQuestionsOfExamQuery : IQuery<QuestionsOfExamDto>
    {
        public int ExamId { get; set; }    
        public string ExamCreatorUserName { get; set; } 
    }
}
