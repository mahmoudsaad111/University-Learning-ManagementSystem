using Contract.Dto.MCQs;
using Contract.Dto.TFQs;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Dto.Exams
{
    public class QuizesOrMidtermsToCourceCycleOfProfDto
    {
        public int CourseCylceId { get; set; }
        public int ExamId { get; set; }
        public string ExamName { get; set; }
        public string ExamTitle { get; set; } 
        public int ExamFullMarks { get; set; }
        public ExamType ExamType { get; set; }
        public DateTime StartedAt { get; set; }
        public TimeSpan DeadLine { get; set; }
        public IEnumerable<NameIdDto> StudentsAttendExam { get; set; }
    }
}
