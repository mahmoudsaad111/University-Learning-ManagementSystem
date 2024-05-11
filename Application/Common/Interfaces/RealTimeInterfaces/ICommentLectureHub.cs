using Application.Common.Interfaces.RealTimeInterfaces.CommentInLecture;
using Application.Common.Interfaces.RealTimeInterfaces.CommentReplyInLecture;
using Application.Common.Interfaces.RealTimeInterfaces.PostInCourse;
using Application.Common.Interfaces.RealTimeInterfaces.PostReplyInCourseCycle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.RealTimeInterfaces
{
    public interface ICommentLectureHub
    {
        public Task AddCommentInLecture(AddCommentInLectureSenderMessage commentMessage);
        public Task UpdateCommentInLecture(UpdateCommentInLectureSenderMessage commentMessage);
        public Task DeleteCommentInLecture(DeleteCommentInLectureSenderMessage commentMessage);

        public Task AddCommentReplyInLecture(AddCommentReplyInLectureSenderMessage commentReplyMessage);
        public Task UpdateCommentReplyInLecture(UpdateCommentReplyInLectureSenderMessage commentReplyMessage);

        public Task DeleteCommentReplyInLecture(DeleteCommentReplyInLectureSenderMessage commentReplyMessage);

    }
}
