using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.RealTimeInterfaces.PostReplyInSection
{
    public class DeletePostReplyInSectionSenderMessage
    {
        public string SenderUserName { get; set; }
        public int SectionId { get; set; }
        public int PostId { get; set; }
        public int PostReplyId { get; set;}
    }
}
