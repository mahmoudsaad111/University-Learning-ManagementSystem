using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Dto.StudentExamDto
{
    public class StudentAttendExamDto
    {
        public int StudentId { get; set; }
        public string StudentFirstName { get; set; }   
        public string StudentSecondName { get; set; }
        public string StudentThirdName { get; set; }
        public string StudentFourthName { get; set; }
        public string StudentImageUrl { get; set; } 
        public string StudentUserName { get; set; }
        public int StudentMarks {  get; set; }  



    }
}
