using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.RealTimeInterfaces.CommentReplyInLecture
{
    public class UpdateCommentReplyInLectureReceiverMessage
    {
        public string CommentReplyContent { get; set; }
        public int CommentId { get; set; }
        public int CommentReplyId { get; set;  }   
    }
}
