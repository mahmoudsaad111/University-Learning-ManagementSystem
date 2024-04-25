using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Dto.StudentSection
{
    public class AddStudentToSectionDto
    {
        public string StudentUserName { get; set; } 
        public int CourseCylceId {  get; set; } 
        public int SectionId { get; set; }
    }
}
