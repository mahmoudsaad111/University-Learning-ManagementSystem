using Contract.Dto.QuestionsOfExam;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Dto.TFQs
{
    public class TFQDto : QuestionDto
    {
        public bool IsTrue { get; set; }

        public TrueFalseQuestion GetTFQ()
        {
            return new TrueFalseQuestion
            {
                IsTrue = IsTrue,
                Text = Text,
                ExamId = ExamId,
                Degree = Degree
            };
        }
    }
}
