using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class MultipleChoiceQuestion : Question
    {
        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public string OptionC { get; set; }
        public string OptionD { get; set; }
        public MCQOptions CorrectAnswer { get; set; } // Option letter

        [JsonIgnore]
        public ICollection<StudentAnswerInMCQ> StudentAnswerInMCQ { get; set; }

    }

}
