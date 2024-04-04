using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Dto.AssignementAnswers
{
    public class AssignementAnswerDto
    {
        public string Description { get; set; }
        public int StudentId { get; set; }  
        public int AssignementId { get; set; }

        public AssignmentAnswer GetAssignementAnswer()
        {
            return new AssignmentAnswer
            {
                Description = Description,
                //   CreatedBy = CreatedBy,
                StudentId = StudentId,
                AssignmentId = AssignementId

            };
        }
    }
}
