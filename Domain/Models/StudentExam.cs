using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class StudentExam
    {
        public int StudentExamId { get; set; }

        public int MarkOfStudentInExam {  get; set; }   
        public DateTime SubmitedAt { get; set; }  
        public int StudentId { get; set; }  
        public Student Student { get; set; }    

        public int ExamId { get; set; } 
        public Exam Exam { get; set; }

        public ICollection<StudentAnswerInMCQ> StudentMcqAnswersOfExam { get; set; }    
        public ICollection<StudentAnswerInTFQ> StudnetTfqAnswersOfExam { get; set; }   

    }
}
