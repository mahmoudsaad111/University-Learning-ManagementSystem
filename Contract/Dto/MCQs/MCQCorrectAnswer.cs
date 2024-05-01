using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Dto.MCQs
{
    public class MCQCorrectAnswer
    {
        public int QuestionId { get; set; }
        public int ExamId { get; set; }
        public int Degree { get; set; }
        public MCQOptions MCQCorrectOption { get; set; }
    }
}
