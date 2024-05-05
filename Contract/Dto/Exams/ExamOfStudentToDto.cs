using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Dto.Exams
{
    public class ExamOfStudentToDto
    {
        public int ExamId { get; set; }
        public string ExamName { get; set; }
        public string ExamTitle { get; set; }
        public ExamType ExamType { get; set; }
        public DateTime StartedDate { get; set; }
        public int MarksOfStudent { get; set; }
        public int FullMarksOfExam { get; set; }      
    }
}
