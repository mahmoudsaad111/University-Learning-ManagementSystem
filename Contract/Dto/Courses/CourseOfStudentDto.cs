using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Dto.Courses
{
    public class CourseOfStudentDto
    {
        public int GroupId {  get; set; }
        public string GroupName { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; }  
        public int ProfessorId {  get; set; }
        public string ProfessorUserName {  get; set; }
        public string ProfessorFirstName { get; set; }
        public string ProfessorSecondName { get; set; }
        public string ProfessorImageUrl { get; set; }   
        public int CourseCycleId {  get; set; }
        public int TotalMarksOfStudent { get; set; }
        
    }
}
