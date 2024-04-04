using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class AssignmentAnswer
    {
        // all the commented lines strated by & symbol are not needed so i commented it to update database

        public int AssignmentAnswer_id { get; set; }       
        public int Mark { get; set; }
        //&   public DateTime DateOfAnswer { get; set; }
        public string Description { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        //& public string CreatedBy { get; set; } = null!;
        //& public string Url { get; set; } = null!;
        public int StudentId { get; set; }
        public Student  Student { get; set; }
        public int AssignmentId { get; set; }
        public Assignment Assignment { get; set; } = null!; 

        public ICollection<AssignmentAnswerResource> AssignmentAnswerResources { get; set; }

    }
}
