using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Dto.CourseCycles
{
    public class CourseCycleWithProfInfoDto
    {
        // benifts are Named info not Id's
        // uses when user filter by CourseId and GroupId to CRUD in sections
        public int CourseCycleId { get; set; }
        public string ProfessorUserName { get; set; }  
        public string ProfessorFirstName { get; set; }
        public string ProfessorSecondName { get; set; }
        public string ProfessorImageUrl { get; set; }   
    }
}
