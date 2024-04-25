using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Dto.Sections
{
    public class SectionOfCourseCycleDto
    {
        public int SectionId { get; set; }  
        public string SectionName { get; set; }
        public string InstructorFirstName {  get; set; }    
        public string InstructorSecondName { get; set;}
        public string InsturctorUserName { get; set; }  
        public string InstructorUrlImage { get; set; }  
        
    }
}
