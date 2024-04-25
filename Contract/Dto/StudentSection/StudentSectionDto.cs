using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Dto.StudentSection
{
    public class StudentSectionDto
    {
        public int StudentCourseCycleId {  get; set; } 
        public int SectionId {  get; set; }
        public int TotalMarks { get; set; }

    }
}
