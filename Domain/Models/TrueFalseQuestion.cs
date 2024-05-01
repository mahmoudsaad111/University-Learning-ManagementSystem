using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class TrueFalseQuestion : Question
    {
        public bool IsTrue { get; set; } // True or False answer

        [JsonIgnore]
        public ICollection<StudentAnswerInTFQ> StudentAnswerInTFQs { get; set; }
    }
}
