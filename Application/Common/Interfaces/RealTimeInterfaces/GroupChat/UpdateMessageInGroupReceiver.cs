using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.RealTimeInterfaces.GroupChat
{
    public class UpdateMessageInGroupReceiver
    {
        public int MessageId { get; set;  }
        public string MessageContent { get; set; }
    }
}
