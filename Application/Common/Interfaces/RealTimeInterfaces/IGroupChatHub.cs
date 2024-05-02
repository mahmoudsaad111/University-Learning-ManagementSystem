using Application.Common.Interfaces.RealTimeInterfaces.GroupChat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.RealTimeInterfaces
{
    public interface IGroupChatHub
    {
        public void AddMessageToGroup(AddMessageToGroupSender postMessage);
        public void UpdateMessageInGroup(UpdateMessageInGroupSender postMessage);
        public void DeleteMessageFromGroup(DeleteMessageInGroupSender postMessage);
    }
}
