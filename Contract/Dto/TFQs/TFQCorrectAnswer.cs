using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Dto.TFQs
{
    public class TFQCorrectAnswer
    {
        public int QuestionId { get; set; }
        public bool IsTrue { get; set; }
        public int Degree { get; set; }
        public int ExamId { get; set; }


    }
}
