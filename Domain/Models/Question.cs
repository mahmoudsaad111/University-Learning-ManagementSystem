using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Models
{
    public abstract class Question
    {
        public int QuestionId { get; set; } 
        public string Text { get; set; }

        public int ExamId { get; set; }
        [JsonIgnore]
        public Exam Exam { get; set; }

        public int Degree { get; set; } 
    }
}
