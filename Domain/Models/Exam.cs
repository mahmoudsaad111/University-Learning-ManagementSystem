using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Exam
    {
        public int ExamId { get; set; }
        public string Name { get; set; } = null!;
        public int FullMark { get; set; } = 0; 
        public string Title { get; set; } = null!;  
        public DateTime CreatedAt { get; set; }
        public DateTime StratedAt { get; set; }
        public TimeSpan DeadLine { get; set; }

        public int ExamPlaceId { get; set; }
        [JsonIgnore]
        public ExamPlace ExamPlace { get; set; }
        public ICollection<StudentExam> StudentsAttendExam { get; set; }    
        public ICollection<TrueFalseQuestion> TrueFalseQuestions { get; set; }  
        public ICollection<MultipleChoiceQuestion> MultipleChoiceQuestions { get; set; }
         
      
    }
}
