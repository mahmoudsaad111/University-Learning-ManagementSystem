using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.RealTimeInterfaces.PostInCourse
{
    public class UpdatePostInCourseCycleSenderMessage
    {
        public string SenderUserName { get; set; }
        public int PostId { get; set; }
        public int CourseCycleId { get; set; }
        public string PostContent { get; set; }

    }
}
