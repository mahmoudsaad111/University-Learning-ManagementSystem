using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class StudentNote
    {
        public int StudentNoteId { get; set; }   
        public string Content { get; set; } = null!;

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt{ get; set; }

        public int LectureId { get; set; }
        public Lecture Lecture { get; set; } = null!;   
        public int StudentId { get; set; }

        public Student Student { get; set; }=null!;

    }
}
