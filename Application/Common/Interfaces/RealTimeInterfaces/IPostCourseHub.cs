using Application.Common.Interfaces.RealTimeInterfaces.PostInCourse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.RealTimeInterfaces
{
    public interface IPostCourseCycleHub
    {
        public void AddPostInCourseCycle(AddPostInCourseCycleSenderMessage postMessage);
        public void UpdatePostInCourseCycle(UpdatePostInCourseCycleSenderMessage postMessage);
        public void DeletePostInCourseCycle(DeletePostInCourseCycleSenderMessage postMessage);
    }
}
