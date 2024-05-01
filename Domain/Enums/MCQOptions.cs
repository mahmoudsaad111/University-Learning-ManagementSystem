using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public enum MCQOptions
    {
        OptionA=1,
        OptionB,
        OptionC,
        OptionD,
        NotChoosed // this is required if student attend the exam doesnot answer this question
                   // used when submit student answer in StudentQuestionAnswer dtos in database
    }
}
