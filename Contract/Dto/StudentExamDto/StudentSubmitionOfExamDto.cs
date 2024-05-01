using Contract.Dto.MCQs;
using Contract.Dto.TFQs;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Dto.StudentExamDto
{
    public class StudentSubmitionOfExamDto
    {
        public int ExamId { get; set; }
        public string StudentUserName { get; set; }
        public List<MCQStudentSubmitionDto> StudentMCQAnswers { get; set; }
        public List<TFQStudentSubmitionDto> StudentTFQAnswers { get; set; }
    }
}
