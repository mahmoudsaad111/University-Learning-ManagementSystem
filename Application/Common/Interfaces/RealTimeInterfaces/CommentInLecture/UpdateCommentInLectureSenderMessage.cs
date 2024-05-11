using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.RealTimeInterfaces.CommentInLecture
{
    public class UpdateCommentInLectureSenderMessage
    {
        public string SenderUserName { get; set; }
        public string CommentContent { get; set; }
        public int CommentId { get; set; }
        public int LectureId { get; set; }
    }
}
