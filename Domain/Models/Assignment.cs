using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Assignment
    {

        // all the commented lines strated by & symbol are not needed so i commented it to update database
        public int AssignmentId { get; set;}
        public string Name { get; set; } = null!;
        public string Description { get; set; }
     
        //& public string ModelAnswerUrl { get; set; } = null!; 
        public int FullMark {  get; set; }  
        public DateTime StartedAt { get; set; }    
        public TimeSpan DeadLine { get; set; }  

       //& public string UrlOfResource { get; set; } = null!;

        public int SectionId { get; set; }
        public Section Section { get; set; }    
        public ICollection<AssignmentAnswer> AssignmentAnswers { get; set; } = null!; 
        public ICollection<AssignmentResource> AssignmentResources { get; set; } 
        
    }
}
