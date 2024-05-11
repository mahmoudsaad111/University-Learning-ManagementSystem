using Application.Common.Interfaces.Presistance;
using Application.Common.Interfaces.RealTimeInterfaces;
using Application.Common.Interfaces.RealTimeInterfaces.PostInCourse;
using Application.Common.Interfaces.RealTimeInterfaces.PostInSection;
using Domain.Enums;
using Domain.Models;
using Microsoft.AspNetCore.SignalR;
  
using Application.Common.Interfaces.RealTimeInterfaces;
using Application.Common.Interfaces.RealTimeInterfaces.PostReplyInCourseCycle;


namespace Infrastructure.RealTimeServices
{
    public class PostCourseCycleHub : Hub, IPostCourseCycleHub
    {
        private readonly IUnitOfwork unitOfwork;
        private readonly ICheckDataOfRealTimeRequests checkDataOfRealTimeRequests;

        public PostCourseCycleHub(IUnitOfwork unitOfwork, ICheckDataOfRealTimeRequests checkDataOfRealTimeRequests)
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
                var CourseCycleIdString = Context?.GetHttpContext()?.Request.Query["CourseCycleId"];

                if (userName is not null && !string.IsNullOrEmpty(CourseCycleIdString))
                {
                    var TypeOfuserAndId = await checkDataOfRealTimeRequests.GetTypeOfUserAndHisId(userName);

                    if (TypeOfuserAndId is not null & int.TryParse(CourseCycleIdString, out int CourseCycleId))
                    {
                        bool IfUserInThisCourse = await checkDataOfRealTimeRequests.CheckIfUserInCourseCycle(CourseCycleId: CourseCycleId,
                            UserId: TypeOfuserAndId.Item2.Id, typesOfUsers: TypeOfuserAndId.Item1);

                        // Add the user to the corresponding section group
                        if (IfUserInThisCourse)
                        {
                            await Groups.AddToGroupAsync(Context.ConnectionId, $"CourseCycle-{CourseCycleId}");
                        }
                    }
                }
            }
            await base.OnConnectedAsync();
        }
        public async Task AddPostInCourseCycle(AddPostInCourseCycleSenderMessage postMessage)
        {
            var TypeOfuserAndId = await checkDataOfRealTimeRequests.GetTypeOfUserAndHisId(postMessage.SenderUserName);

            if (TypeOfuserAndId is not null && (TypeOfuserAndId.Item1 == TypesOfUsers.Professor))
            {

                User user = TypeOfuserAndId.Item2;

                var post = new Post
                {
                    Content = postMessage.PostContent,
                    CreatedBy = postMessage.SenderUserName,
                    IsProfessor = true ? TypeOfuserAndId.Item1 == TypesOfUsers.Professor : false,
                    CourseCycleId = postMessage.CourseCycleId,
                    PublisherId = user.Id

                };
                try
                {
                    var postInDB = await unitOfwork.PostRepository.CreateAsync(post);
                    await unitOfwork.SaveChangesAsync();
                    var postMessageforClinets = new PostAddReceiverMessage
                    {
                        SenderImage = user.ImageUrl,
                        SenderName = user.FullName,
                        SenderUserName = user.UserName,
                        PostContent = post.Content,
                        PostId = postInDB.PostId

                    };
                    await Clients.Group($"CourseCycle-{postMessage.CourseCycleId}").SendAsync("AddCourseCyclePost", postMessageforClinets);
                }
                catch (Exception ex)
                {
                }
            }


        }

        public async Task DeletePostInCourseCycle(DeletePostInCourseCycleSenderMessage postMessage)
        {
            var TypeOfuserAndId = await checkDataOfRealTimeRequests.GetTypeOfUserAndHisId(postMessage.SenderUserName);

            var post = await unitOfwork.PostRepository.GetByIdAsync(postMessage.PostId);

            // If there is post with the given id and the publisher is the same who want to delete it then OK 

            if (post != null && post.PublisherId == TypeOfuserAndId.Item2.Id)
            {
                try
                {
                    bool IsDeleted = await unitOfwork.PostRepository.DeleteAsync(postMessage.PostId);
                    if (IsDeleted)
                    {
                        await unitOfwork.SaveChangesAsync();

                        PostDeleteReceiverMessage postReceiverMessage = new PostDeleteReceiverMessage
                        {
                            PostId = postMessage.PostId,
                        };

                        await Clients.Group($"CourseCycle-{postMessage.CourseCycleId}").SendAsync("UpdateCourseCyclePost", postReceiverMessage);
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }

        public async Task UpdatePostInCourseCycle(UpdatePostInCourseCycleSenderMessage postMessage)
        {
            var TypeOfuserAndId = await checkDataOfRealTimeRequests.GetTypeOfUserAndHisId(postMessage.SenderUserName);

            var post = await unitOfwork.PostRepository.GetByIdAsync(postMessage.PostId);

            // If there is post with the given id and the publisher is the same who want to delete it then OK 

            if (post != null && post.PublisherId == TypeOfuserAndId.Item2.Id)
            {
                post.Content = postMessage.PostContent;
                try
                {
                    bool IsUpdated = await unitOfwork.PostRepository.UpdateAsync(post);
                    if (IsUpdated)
                    {
                        PostUpdateReceiverMessage postReceiverMessage = new PostUpdateReceiverMessage
                        {
                            PostId = postMessage.PostId,
                            PostContent = postMessage.PostContent,
                        };

                        await Clients.Group($"CourseCycle-{postMessage.CourseCycleId}").SendAsync("UpdateCourseCyclePost", postReceiverMessage);
                    }
                }
                catch (Exception ex) { }

            }
        }
        public async Task AddPostReplyInCourseCycle(AddPostReplyInCourseCycleSenderMessage postReplyMessage)
        {
            var TypeOfuserAndId = await checkDataOfRealTimeRequests.GetTypeOfUserAndHisId(postReplyMessage.SenderUserName);

            if (TypeOfuserAndId is not null)
            {

                User user = TypeOfuserAndId.Item2;

                var postReply = new PostReply
                {
                    Content = postReplyMessage.PostReplyContent,
                    CreatedBy = postReplyMessage.SenderUserName,
                    CreatedAt = DateTime.Now,
                    PostId = postReplyMessage.PostId,
                    ReplierId = user.Id,

                };
                try
                {
                    var postReplyInDB = await unitOfwork.PostReplyRepository.CreateAsync(postReply);
                    await unitOfwork.SaveChangesAsync();
                    var postReplyMessageforClinets = new AddPostReplyInCourseCycleReceiverMessage
                    {
                        SenderImageUrl = user.ImageUrl,
                        PostReplyContent = user.FullName,
                        PostReplyId = postReplyInDB.PostReplyId,
                        SenderUserName = user.UserName,
                        SenderName = user.FullName,
                        PostId = postReplyInDB.PostId

                    };
                    await Clients.Group($"CourseCycle-{postReplyMessage.CourseCycleId}").SendAsync("AddPostReplyInCourseCycle", postReplyMessageforClinets);
                }
                catch (Exception ex)
                {
                }
            }

        }

        public async Task UpdatePostReplyInCourseCycle(UpdatePostReplyInCourseCycleSenderMessage postReplyMessage)
        {
            var TypeOfuserAndId = await checkDataOfRealTimeRequests.GetTypeOfUserAndHisId(postReplyMessage.SenderUserName);

            var postReply = await unitOfwork.PostReplyRepository.GetByIdAsync(postReplyMessage.PostReplyId);

            if (postReply != null && postReply.ReplierId == TypeOfuserAndId.Item2.Id)
            {
                postReply.Content = postReplyMessage.PostReplyContent;
                try
                {
                    bool IsUpdated = await unitOfwork.PostReplyRepository.UpdateAsync(postReply);

                    if (IsUpdated)
                    {
                        await unitOfwork.SaveChangesAsync();

                        var postReplyReceiverMessage = new UpdatePostReplyInCourseCycleReceiverMessage
                        {
                            PostId = postReply.PostId,
                            PostReplyContent = postReply.Content,
                            PostReplyId = postReply.PostReplyId,
                        };

                        await Clients.Group($"CourseCycle-{postReplyMessage.CourseCycleId}").SendAsync("UpdatePostReplyInCourseCycle", postReplyReceiverMessage);
                    }
                }
                catch (Exception ex) { }

            }
        }

        public async Task DeletePostReplyInCourseCycle(DeletePostReplyInCourseCycleSenderMessage postReplyMessage)
        {
            var TypeOfuserAndId = await checkDataOfRealTimeRequests.GetTypeOfUserAndHisId(postReplyMessage.SenderUserName);

            var postReply = await unitOfwork.PostReplyRepository.GetByIdAsync(postReplyMessage.PostReplyId);

            // If there is post with the given id and the publisher is the same who want to delete it then OK 

            if (postReply != null && (postReply.ReplierId == TypeOfuserAndId.Item2.Id || TypeOfuserAndId.Item1 == TypesOfUsers.Professor))
            {
                try
                {
                    bool IsDeleted = await unitOfwork.PostReplyRepository.DeleteAsync(postReplyMessage.PostReplyId);
                    if (IsDeleted)
                    {
                        await unitOfwork.SaveChangesAsync();

                        var postReplyReceiverMessage = new DeletePostReplyInCourseCycleReceiverMessage
                        {
                            PostId = postReplyMessage.PostId,
                            PostReplyId = postReplyMessage.PostReplyId,
                        };

                        await Clients.Group($"CourseCycle-{postReplyMessage.CourseCycleId}").SendAsync("DeleteCoursePost", postReplyReceiverMessage);
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }
    }
}
