using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Dto.QuestionsOfExam
{
    public class QuestionDto
    {
        public int ExamId { get; set; }
        public string Text { get; set; }    
        public int Degree { get; set; } 
    }
}
