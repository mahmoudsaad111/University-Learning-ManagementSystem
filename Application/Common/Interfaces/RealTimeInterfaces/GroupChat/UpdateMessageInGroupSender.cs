using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.RealTimeInterfaces.GroupChat
{
    public class UpdateMessageInGroupSender
    {
        public string SenderUserName { get; set; }
        public int GroupId { get; set; }
        public string MessageContent { get; set; }

        public int MessageId { get; set; }
    }
}
