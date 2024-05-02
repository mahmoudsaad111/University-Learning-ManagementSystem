using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.RealTimeInterfaces.PostInSection
{
    public class PostAddReceiverMessage
    {
        public string SenderUserName { get; set; }  
        public String SenderName { get; set; }
        public string SenderImage {  get; set; }

        public String PostContent { get; set; }

        public int PostId { get; set; }  
    }
}
