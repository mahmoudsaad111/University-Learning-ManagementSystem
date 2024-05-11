using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.RealTimeInterfaces.PostReplyInCourseCycle
{
    public class DeletePostReplyInCourseCycleReceiverMessage
    {
        public int PostId { get; set; }
        public int PostReplyId { get; set; }
    }
}
