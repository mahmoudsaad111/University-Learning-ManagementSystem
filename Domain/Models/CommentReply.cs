using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class CommentReply
    {
        public int CommentReplyId { get; set; } 
        
        public string Content { get; set; } = null!; 

      
        public DateTime CreatedAt { get; set; } 
        public DateTime UpdatedAt { get; set;}
        public string CreatedBy { get; set; } = null!;

        public int CommentId { get; set; }
        public Comment Comment { get; set; } = null!;   
        
        public int UserId {  get; set; }  
        
        public User User { get; set; }  
    }
}
