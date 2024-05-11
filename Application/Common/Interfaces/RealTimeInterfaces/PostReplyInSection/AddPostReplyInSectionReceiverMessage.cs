using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.RealTimeInterfaces.PostReplyInSection
{
    public class AddPostReplyInSectionReceiverMessage
    {
        public int PostId { get; set; }
        public int PostReplyId { get; set; }
        public string SenderUserName { get; set; }
        public string SenderName { get; set; }
        public string SenderImageUrl { get; set; }
        public string PostReplyContent { get; set; }

    }
}
