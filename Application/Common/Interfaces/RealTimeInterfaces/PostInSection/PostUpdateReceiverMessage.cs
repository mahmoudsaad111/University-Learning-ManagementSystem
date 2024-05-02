using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.RealTimeInterfaces.PostInSection
{
    public class PostUpdateReceiverMessage
    {
        public int PostId { get; set; }
        public string PostContent { get; set; }
    }
}
