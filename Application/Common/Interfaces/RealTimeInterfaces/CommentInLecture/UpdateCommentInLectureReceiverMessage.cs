using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.RealTimeInterfaces.CommentInLecture
{
    public class UpdateCommentInLectureReceiverMessage
    {
        public int CommenntId { get; set; }
        public string CommentContent { get; set; }

    }
}
