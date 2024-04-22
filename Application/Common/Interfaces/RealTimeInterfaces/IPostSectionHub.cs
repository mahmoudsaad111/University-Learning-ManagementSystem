using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.RealTimeInterfaces
{
    public interface IPostSectionHub
    {
        public void AddPostInSection(int UserId, int SectionId, string PostContent);
        public void UpdatePostInSection(int UserId, int SectionId,int PostId,  string PostContent);
        public void DeletePostInSection(int UserId, int SectionId, int PostId);
    }
}
