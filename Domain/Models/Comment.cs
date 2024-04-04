using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Comment
    {
        public int CommentId { get; set; } 
        public string Content { get; set; } = null!;  
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set;}
        public string CreatedBy { get; set; } = null!;

        public int UserId {  get; set; }  
        
        public User User { get; set; }
        public ICollection<CommentReply> CommentReplies { get; set; } = null!;

        public int LectureId { get; set; }

        public Lecture lecture { get; set; } = null!;
    }
}
