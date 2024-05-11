using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.RealTimeInterfaces.PostReplyInCourseCycle
{
    public class AddPostReplyInCourseCycleSenderMessage
    {
        public string SenderUserName { get; set; }
        public string PostReplyContent { get; set; }

        public int PostId { get; set; }
        public int CourseCycleId { get; set; }

    }
}
