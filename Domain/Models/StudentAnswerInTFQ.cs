using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class StudentAnswerInTFQ
    {
        public int StudentAnswersInTFQId {  get; set; } 
        public int StudentExamId {  get; set; } 
        public StudentExam StudentExam { get; set; }    
        public int TrueFalseQuestionId {  get; set; }
        public TrueFalseQuestion TrueFalseQuestion {  get; set; }   
        public bool StudentAnswer {  get; set; }   
    }
}
