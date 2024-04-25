using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Dto.Courses
{
    public class CourseOfProfessorDto
    {
        public int CourseCycleId { get; set; }  
        public int GroupId {  get; set; }   
        public string GroupName { get; set; }
        public int CourseId { get; set; } 
        public string CourseName { get; set; }  

        public int AcadimicYearId { get; set; }
        public int Year { get; set; }
        public int DepartementId { get; set; }  
        public string DepartmentName { get; set; }  
        public int FacultyId { get; set; }
        public string FacultyName { get; set;}


    }
}
