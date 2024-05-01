using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class StudentAnswerInMCQ
    {
        public int StudentAnswerInMCQId { get; set; }   

        public MCQOptions OptionSelectedByStudent {  get; set; } 
        public int StudentExamId { get; set; }  
        public StudentExam StudentExam { get; set; }

        public int MultipleChoiceQuestionId {get; set; }    
        public  MultipleChoiceQuestion MultipleChoiceQuestion { get; set; } 
    }
}
