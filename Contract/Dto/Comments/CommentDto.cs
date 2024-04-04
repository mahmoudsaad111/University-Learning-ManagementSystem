using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Dto.Comments
{
    public class CommentDto
    {       
        public string Content { get; set; } = null!;     
        public string CreatedBy { get; set; } = null!;
        public int UserId { get; set; }
        public int LectureId { get; set; }

        public Comment GetComment()
        {
            return new Comment
            {
                Content = Content,
                CreatedBy = CreatedBy,
                UserId = UserId,
                LectureId = LectureId
            };
        }
    }
}
