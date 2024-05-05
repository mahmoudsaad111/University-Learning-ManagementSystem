using Application.Common.Interfaces.Presistance;
using Application.Common.Interfaces.RealTimeInterfaces;
using Application.Common.Interfaces.RealTimeInterfaces.PostInCourse;
using Application.Common.Interfaces.RealTimeInterfaces.PostInSection;
using Domain.Enums;
using Domain.Models;
using Microsoft.AspNetCore.SignalR;
  
using Application.Common.Interfaces.RealTimeInterfaces;
 

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
        public async void AddPostInCourseCycle(AddPostInCourseCycleSenderMessage postMessage)
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

        public async void DeletePostInCourseCycle(DeletePostInCourseCycleSenderMessage postMessage)
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

        public async void UpdatePostInCourseCycle(UpdatePostInCourseCycleSenderMessage postMessage)
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
    }
}
