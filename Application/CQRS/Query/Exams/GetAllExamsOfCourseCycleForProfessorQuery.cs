using Application.Common.Interfaces.CQRSInterfaces;
using Contract.Dto.Exams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Query.Exams
{
    public class GetAllExamsOfCourseCycleForProfessorQuery  :IQuery<IEnumerable<QuizesOrMidtermsToCourceCycleOfProfDto>>
    {
        public int CourseCycleId { get; set; }  
        public string ProfessorUserName { get; set; }   
    }
}
