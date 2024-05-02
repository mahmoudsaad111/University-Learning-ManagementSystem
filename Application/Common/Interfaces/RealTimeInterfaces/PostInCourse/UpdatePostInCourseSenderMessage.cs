using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.RealTimeInterfaces.PostInCourse
{
    public class UpdatePostInCourseSenderMessage
    {
        public string SenderUserName { get; set; }
        public int PostId { get; set; }
        public int CourseId { get; set; }
        public string PostContent { get; set; }

    }
}
