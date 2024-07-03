using Application.Common.Interfaces.CQRSInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Query.AssignementAnswers
{
    public class GetAssignmentAnswerIdQuery :IQuery<int>
    {
        public int AssessmentId { get; set; }   
        public string StudentUserName { get; set; }
    }
}
