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
        public Task AddMessageToGroup(AddMessageToGroupSender postMessage);
        public Task UpdateMessageInGroup(UpdateMessageInGroupSender postMessage);
        public Task DeleteMessageFromGroup(DeleteMessageInGroupSender postMessage);
    }
}
