using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Dto.Sections
{
    public class SectionsOfCoursesToStudentDto
    {
        public int SectionId { get; set; }
        public string SectionName { get; set; }
        public int TotalMarksOfStudent { get; set; }
        public int StudentSectionId {  get; set; }  
        public string SectionDescreption { get; set; }
        public int InstructorId { get; set; }
        public string InstructorNameFirst { get; set; }
        public string InstructorSecondName { get; set; }
        public string InstructorUserName { get; set; }
        public string InstructorImageUrl {get;set;}
    }
}
