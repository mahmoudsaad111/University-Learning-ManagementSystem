using Application.Common.Interfaces.RealTimeInterfaces.PostInCourse;
using Application.Common.Interfaces.RealTimeInterfaces.PostReplyInCourseCycle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.RealTimeInterfaces
{
    public interface IPostCourseCycleHub
    {
        public Task AddPostInCourseCycle(AddPostInCourseCycleSenderMessage postMessage);
        public Task UpdatePostInCourseCycle(UpdatePostInCourseCycleSenderMessage postMessage);
        public Task DeletePostInCourseCycle(DeletePostInCourseCycleSenderMessage postMessage);
        public Task AddPostReplyInCourseCycle(AddPostReplyInCourseCycleSenderMessage postReplyMessage);
        public Task UpdatePostReplyInCourseCycle(UpdatePostReplyInCourseCycleSenderMessage postReplyMessage);

        public Task DeletePostReplyInCourseCycle(DeletePostReplyInCourseCycleSenderMessage postReplyMessage);

    }
}
