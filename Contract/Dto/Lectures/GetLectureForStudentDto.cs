using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Dto.Lectures
{
    public class GetLectureForStudentDto
    { 
        public string StudentUserName { get; set; } 
        public int SectionId {  get; set; } 
        public int CourseCycleId {  get; set; } 
    }
}
