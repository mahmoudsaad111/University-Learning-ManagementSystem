using Contract.Dto.MCQs;
using Contract.Dto.TFQs;
using Domain.Enums;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Dto.Exams
{
    public class QuizsToSectionOfInstructorDto
    {
        public int SectionId { get; set; }
        public string SectionName {  get; set; }   
        public int ExamId { get; set; } 
        public string ExamName { get; set; }  
        public int ExamFullMarks { get; set; }  
        public DateTime StartedAt {  get; set; }    
        public TimeSpan DeadLine { get; set; }
      //  public IEnumerable<NameIdDto> StudentsAttendExam {  get; set; } 
    }
}
