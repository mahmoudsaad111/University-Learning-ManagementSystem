using Application.Common.Interfaces.Presistance;
using Application.Common.Interfaces.RealTimeInterfaces;
using Application.Common.Interfaces.RealTimeInterfaces.GroupChat;
using Application.Common.Interfaces.RealTimeInterfaces.PostInSection;
using Domain.Enums;
using Domain.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.RealTimeServices
{
    public class GroupChatHub : Hub, IGroupChatHub
    {
        private readonly IUnitOfwork unitOfwork;
        private readonly ICheckDataOfRealTimeRequests checkDataOfRealTimeRequests;

        public GroupChatHub(IUnitOfwork unitOfwork, ICheckDataOfRealTimeRequests checkDataOfRealTimeRequests)
        {
            this.unitOfwork = unitOfwork;
            this.checkDataOfRealTimeRequests = checkDataOfRealTimeRequests;
        }

        public async override Task OnConnectedAsync()
        {
            if (Context is not null)
            {
                string connectionId = Context.ConnectionId;
                var userName = Context?.User?.Identity?.Name;
                var GroupIdString = Context?.GetHttpContext()?.Request.Query["GroupId"];

                if (userName is not null && !string.IsNullOrEmpty(GroupIdString))
                {
                    var TypeOfuserAndId = await checkDataOfRealTimeRequests.GetTypeOfUserAndHisId(userName);

                    if (TypeOfuserAndId is not null & int.TryParse(GroupIdString, out int GroupId))
                    {
                        bool IfUserInThisGroup = await checkDataOfRealTimeRequests.CheckIfUserInGroup(GroupId: GroupId,
                            UserId: TypeOfuserAndId.Item2.Id, typesOfUsers: TypeOfuserAndId.Item1);

                        // Add the user to the corresponding section group
                        if (IfUserInThisGroup)
                        {
                            await Groups.AddToGroupAsync(Context.ConnectionId, $"Group-{GroupId}");
                        }
                    }
                }
            }
            await base.OnConnectedAsync();
        }

        public async Task AddMessageToGroup(AddMessageToGroupSender groupMessage)
        {
            var TypeOfuserAndId = await checkDataOfRealTimeRequests.GetTypeOfUserAndHisId(groupMessage.SenderUserName);

            if (TypeOfuserAndId is not null)
            {

                User user = TypeOfuserAndId.Item2;

                var Message = new Message
                {
                    CreatedOn = DateTime.Now,
                    UserId = user.Id,
                    MessageContent = groupMessage.MessageContent,
                    GroupId = groupMessage.GroupId,

                };
                try
                {
                    var message = await unitOfwork.MessageRepository.CreateAsync(Message);
                    await unitOfwork.SaveChangesAsync();
                    var groupMessageReceiver = new AddMessageToGroupReceiver
                    {
                        MessageContent = groupMessage.MessageContent,
                        SenderUserName = groupMessage.SenderUserName,
                        SenderId = user.Id,
                        SenderImage = user.ImageUrl,
                        SenderName = user.FullName,
                        MessageId = message.MessageId,
                        CreatedOn = Message.CreatedOn,
                    };

                    await Clients.Group($"Group-{groupMessage.GroupId}").SendAsync("NewMessageInGroup", groupMessageReceiver);
                }
                catch (Exception ex)
                {

                }
            }

        }

        public async Task DeleteMessageFromGroup(DeleteMessageInGroupSender GroupMessage)
        {
            var TypeOfuserAndId = await checkDataOfRealTimeRequests.GetTypeOfUserAndHisId(GroupMessage.SenderUserName);

            var message = await unitOfwork.MessageRepository.GetByIdAsync(GroupMessage.MessageId);

            // If there is post with the given id and the publisher is the same who want to delete it then OK 

            if (message != null && message.UserId == TypeOfuserAndId.Item2.Id)
            {
                try
                {
                    bool IsDeleted = await unitOfwork.MessageRepository.DeleteAsync(message.MessageId);
                    if (IsDeleted)
                    {
                        await unitOfwork.SaveChangesAsync();
                        DeleteMessageInGroupReceiver DeleteMessageReceiver = new DeleteMessageInGroupReceiver
                        {
                            MessageId = message.MessageId
                        };

                        await Clients.Group($"Group-{GroupMessage.GroupId}").SendAsync("DeleteMessageInGroup", DeleteMessageReceiver);
                    }
                }
                catch (Exception ex) { }
            }


        }


        public async Task UpdateMessageInGroup(UpdateMessageInGroupSender GroupMessage)
        {
            var TypeOfuserAndId = await checkDataOfRealTimeRequests.GetTypeOfUserAndHisId(GroupMessage.SenderUserName);

            var message = await unitOfwork.MessageRepository.GetByIdAsync(GroupMessage.MessageId);

            // If there is post with the given id and the publisher is the same who want to delete it then OK 

            if (message != null && message.UserId == TypeOfuserAndId.Item2.Id)
            {
                message.MessageContent = GroupMessage.MessageContent;

                try
                {
                    bool IsUpdated = await unitOfwork.MessageRepository.UpdateAsync(message);

                    if (IsUpdated)
                    {
                        await unitOfwork.SaveChangesAsync();
                        UpdateMessageInGroupReceiver UpdateMessageReceiver = new UpdateMessageInGroupReceiver
                        {
                            MessageId = message.MessageId,
                            MessageContent = message.MessageContent
                        };

                        await Clients.Group($"Group-{GroupMessage.GroupId}").SendAsync("UpdateMessageInGroup", UpdateMessageReceiver);
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }
    }
}
