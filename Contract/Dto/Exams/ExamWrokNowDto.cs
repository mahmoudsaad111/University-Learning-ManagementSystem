using Contract.Dto.MCQs;
using Contract.Dto.TFQs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Dto.Exams
{
    public class ExamWrokNowDto
    {
        public int ExamId { get; set; } 
        public string ExamName { get; set; }    
        public string ExamTitle { get; set; } 
        public int ExamFullMarks { get; set; }  
        public DateTime ExamDateTime { get; set; }
        public TimeSpan ExamDeadLine { get; set; }
        public IEnumerable<MCQTextOptionsDto> mCQTextOptionsDtos { get; set; }
        public IEnumerable<TFQTextDto> tFQTextOptionsDtos { get; set; }
    }
}
