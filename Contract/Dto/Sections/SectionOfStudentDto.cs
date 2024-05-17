using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Dto.Sections
{
    public class SectionOfStudentDto
    {
        public int SectionId { get; set; } 
        public string SectionName { get; set; }
        public string SectionDescription { get; set; }
        public int InstructorId { get; set; }
        public string InstructorFirstName { get; set; }
        public string InstructorSecondName { get; set; }
        public string InstructorUserName { get; set; }  
        public string InstructorImageUrl { get; set; }  
    }
}
