using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Dto.CommentReplies
{
    public class CommentReplyDto
    {
        public string Content { get; set; } = null!;
        public string CreatedBy { get; set; } = null!;
        public int CommentId { get; set; }
        public int UserId { get; set; }
        public CommentReply GetCommentReply()
        {
            return new CommentReply
            {
                Content = Content,
                CreatedBy = CreatedBy,
                CommentId = CommentId,
                UserId = UserId
            };
        }
    }
}
