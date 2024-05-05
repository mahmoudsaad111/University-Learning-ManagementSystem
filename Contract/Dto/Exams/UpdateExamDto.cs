using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Dto.Exams
{
    public class UpdateExamDto
    {
        public string Name { get; set; }
       // public int FaullMarks { get; set; }
        public string Title { get; set; }
        public DateTime SartedAt { get; set; }
        public TimeSpan DeadLine { get; set; }

        // to prevent updates on ExamType or foreign keys
    }
}
