using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Exam
    {
        public int ExamId { get; set; }
        public string Name { get; set; } = null!;

        public string ModelAnswerUrl { get; set; } = null!;
        public string Url { get; set; } = null!;
        public int FullMark { get; set; }
        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public DateTime DeadLine { get; set; }

        public ICollection<ExamAnswer> ExamAnswers { get; set; } = null!;
        public int CourseCycleId { get; set; }

        public CourseCycle CourseCycle { get; set; }
    }
}
