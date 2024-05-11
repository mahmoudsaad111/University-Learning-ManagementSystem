using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.RealTimeInterfaces.PostReplyInSection
{
    public class AddPostReplyInSectionSenderMessage
    {
        public int PostId { get; set; }
        public int SectionId { get; set; }
        public string PostReplyContent { get; set; }
        public string SenderUserName { get; set; }

    }
}
