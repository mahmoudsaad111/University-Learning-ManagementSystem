using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Contract.Dto.Sections
{
    public class SectionsOfProfessorDto
    {
        public int CourseCycleId { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        
        public int GroupId {  get; set; }   
        public string GroupName { get; set; }   

        public int SectionId {  get; set; } 
        public string SectionName { get; set; }    
        public string SectionDescreption { get; set; }

        public string InstructorFirstName {  get; set; }
        public string InstructorSecondName { get; set; }
        public string InstructorImageUrl { get; set; }  
        public string InstructorUserName {  get; set; } 


        public int AcadimicYearId { get; set; }
        public int Year { get; set; }

        public int DepartementId { get; set; }
        public string DepartementName { get; set; }    
        public int FacultyId { get; set; }
        public string FacultyName { get; set; }
    }
}
