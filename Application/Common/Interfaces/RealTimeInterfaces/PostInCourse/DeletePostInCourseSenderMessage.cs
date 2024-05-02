using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.RealTimeInterfaces.PostInCourse
{
    public class DeletePostInCourseSenderMessage
    {
        public string SenderUserName {  get; set; }
        public int PostId { get; set; }

        public int CourseId { get; set; }
    }
}
