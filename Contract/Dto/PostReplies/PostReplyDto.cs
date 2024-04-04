using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Dto.PostReplies
{
    public class PostReplyDto
    {
        public string Content { get; set; }
        public string CreatedBy { get; set; }
        public int PostId { get; set; }
        public int ReplierId { get; set; }

        public PostReply GetPostReply ()
        {
            return new PostReply
            {
                Content = Content,
                CreatedBy = CreatedBy,
                ReplierId = ReplierId,
                PostId = PostId
            };
        }
    }
}
