using Contract.Dto.MCQs;
using Contract.Dto.TFQs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Dto.StudentExamDto
{
    public class StudentAnswersOfExamDto
    {
        public string StudentFullName { get; set; } 
        public int StudentId { get; set; }
        public int StudentTotalMarks { get; set; }  
        public string StudentUserName { get; set; } 
        public string StudentImageUrl { get; set; } 
        public DateTime StudentSubmissionDate {  get; set; }    
        public IEnumerable<MCQInfoWithStudentAnswerDto> MCQInfoWithStudentAnswerDtos { get; set; }
        public IEnumerable<TFQInfoWithStudentAnswerDto> TFQInfoWithStudentAnswerDtos { get; set; }  

    }
}
