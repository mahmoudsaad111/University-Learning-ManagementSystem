using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.RealTimeInterfaces.PostReplyInSection
{
    public class UpdatePostReplyInSectionReceiverMessage
    {
        public int PostId { get; set; }
        public int PostReplyId {  get; set; }
        public string PostReplyContent { get; set; }
    }
}
