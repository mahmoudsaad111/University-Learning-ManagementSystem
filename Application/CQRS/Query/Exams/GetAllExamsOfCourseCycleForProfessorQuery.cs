using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Query.Exams
{
    public class GetAllExamsOfCourseCycleForProfessorQuery 
    {
        public int ExamId { get; set; } 
        public string ProfessorUserName { get; set; }   
    }
}
