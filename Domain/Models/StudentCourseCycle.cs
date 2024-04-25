using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class StudentCourseCycle

    { 
        public int StudentCourseCycleId { get; set; }
        public int StudentId { get; set; }  
        public Student Student { get; set; }    
        public int CourseCycleId { get; set; }  
        public CourseCycle CourseCycle { get; set; }
        public int MarksOfStudent {  get; set; }

        public ICollection<StudentSection> SectionsOfStudent { get; set; }
         
    }
}
