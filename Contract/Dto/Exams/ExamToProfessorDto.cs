﻿using Contract.Dto.MCQs;
using Contract.Dto.TFQs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Dto.Exams
{
    public class ExamToProfessorDto
    {
        public int SectionId { get; set; }
        public string SectionName { get; set; }
 
        public int ExamId { get; set; }
        public string ExamName { get; set; }
        public int ExamFullMarks { get; set; }
        public DateTime StartedAt { get; set; }
        public TimeSpan DeadLine { get; set; }
        public IEnumerable<TFQTextAsnwerDto> TFQs { get; set; }
        public IEnumerable<MCQTextOPtionsAnswerDto> MCQs { get; set; }
        public IEnumerable<NameIdDto> StudentsAttendExam { get; set; }
    }
}
