using Application.Common.Interfaces.RealTimeInterfaces.PostInSection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.RealTimeInterfaces
{
    public interface IPostSectionHub
    {
        public void AddPostInSection(PostAddSenderMessage postMessage);
        public void UpdatePostInSection(PostUpdateSenderMessage postMessage);
        public void DeletePostInSection(PostDeleteSenderMessage postMessage);
    }
}
