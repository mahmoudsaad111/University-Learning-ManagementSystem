using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Contract.Dto.Sections
{
    public class SectionOfInstructorDto
    {
        public int SectionId { get; set; }
        public string SectionName {  get; set; }    
        public string SectionDescreption { get; set; }  
        public string ProfessorFirstName { get; set;}   
        public string ProfessorSecondName {  get; set;}
        public string ProfessorUserName { get; set; }
        public int CourseId { get; set; }   
        public string CourseName { get; set;}   

        public int GroupId { get; set; }
        public string GroupName { get; set; }

        public int AcadimicYearId { get; set; } 
        public int Year { get; set; }
    }
}
