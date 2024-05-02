using Application.Common.Interfaces.RealTimeInterfaces.PostInCourse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.RealTimeInterfaces
{
    public interface IPostCourseHub
    {
        public void AddPostInCourse(AddPostInCourseSenderMessage postMessage);
        public void UpdatePostInCourse(UpdatePostInCourseSenderMessage postMessage);
        public void DeletePostInCourse(DeletePostInCourseSenderMessage postMessage);
    }
}
