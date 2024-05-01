using Contract.Dto.QuestionsOfExam;
using Domain.Enums;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Dto.MCQs
{
    public class MCQDto : QuestionDto
    {
        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public string OptionC { get; set; }
        public string OptionD { get; set; }
        public MCQOptions CorrectAnswer { get; set; }

        public MultipleChoiceQuestion GetMCQ()
        {
            return new MultipleChoiceQuestion
            {
                OptionA = OptionA,
                OptionB = OptionB,
                OptionC = OptionC,
                OptionD = OptionD,
                CorrectAnswer = CorrectAnswer,
                Degree = Degree,
                ExamId = ExamId,
                Text = Text
            };
        }
    }
}
