using Application.Common.Interfaces.RealTimeInterfaces.PostInSection;
using Application.Common.Interfaces.RealTimeInterfaces.PostReplyInSection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.RealTimeInterfaces
{
    public interface IPostSectionHub
    {
        public Task AddPostInSection(PostAddSenderMessage postMessage);
        public Task UpdatePostInSection(PostUpdateSenderMessage postMessage);
        public Task DeletePostInSection(PostDeleteSenderMessage postMessage);

        public Task AddPostReplyInSection(AddPostReplyInSectionSenderMessage postMessage);

        public Task DeletePostReplyInSection(DeletePostReplyInSectionSenderMessage postMessage);

        public Task UpdatePostReplyInSection(UpdatePostReplyInSectionSenderMessage postMessage);
    }
}
