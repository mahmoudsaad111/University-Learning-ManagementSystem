using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class ExamAnswer
    {
        public int ExamAnswerId { get; set; }

        public String DescriptionOrNote { get; set; }
        public int Mark { get; set; }


        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; } = null!;
        public string Url { get; set; } = null!;
        public int ExamId { get; set; }

        public Exam Exam { get; set; } = null!;
        
        public int StudentId { get; set; }

        public Student Student { get; set; }


    }
}
