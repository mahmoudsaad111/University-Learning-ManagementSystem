using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.RealTimeInterfaces.GroupChat
{
    public class AddMessageToGroupReceiver
    {
        public string SenderUserName {  get; set; }
        public string SenderName { get; set; }

        public string SenderImage {  get; set; }

        public int SenderId { get; set; }   

        public string MessageContent { get; set; }

        public int MessageId { get; set; }

        public DateTime CreatedOn { get; set; }


    }
}
