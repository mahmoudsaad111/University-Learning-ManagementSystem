using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.RealTimeInterfaces.PostReplyInCourseCycle
{
    public class UpdatePostReplyInCourseCycleSenderMessage
    {
        public string SenderUserName { get; set; }
        public int PostReplyId { get; set; }
        public string PostReplyContent {  get; set; }
        public int CourseCycleId { get; set;  }
    }
}
