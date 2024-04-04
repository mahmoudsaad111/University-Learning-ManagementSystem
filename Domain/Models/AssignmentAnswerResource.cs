using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class AssignmentAnswerResource : FileResource
    {
        public int AssignmentAnswerId { get; set; }
        public AssignmentAnswer AssignmentAnswer { get; set; }
    }
}
