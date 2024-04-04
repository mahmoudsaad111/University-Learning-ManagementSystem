using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Lecture
    {
        public int LectureId { get; set; }

        public string Name { get; set; } = null!; 
        public bool HavingAssignment { get; set; } 

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public ICollection<LectureResource> LectureResources { get; set;}
        public ICollection<Student_Lecture> Student_Lectures { get; set; }

        public ICollection<StudentNote> StudentNotes { get; set; }
        public ICollection<Comment>Comments { get; set; }    
        public int? SectionId { get; set; }

        public Section? Section { get; set; }
        public int? CourseCycleId { get; set; }
        public CourseCycle? CourseCycle { get; set; }
    }
}
