using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.RealTimeInterfaces.PostInCourse
{
    public class AddPostInCourseCycleSenderMessage
    {
        public int CourseCycleId { get; set; }   
        public string PostContent { get; set; }

        public string SenderUserName { get; set; }

    }
}
