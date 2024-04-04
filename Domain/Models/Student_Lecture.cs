using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
     // For student attendence in lectures
    public class Student_Lecture
    {
        public int Student_LectureId { get; set; }
       
       public int MarksOfAttendence { get; set; }

       public int LectureId { get; set; }
       public Lecture lecture { get; set; } = null!;   
       public int StudentId { get; set; }

       public Student Student { get; set; }

    }
}
