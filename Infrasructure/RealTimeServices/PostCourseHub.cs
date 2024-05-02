using Application.Common.Interfaces.Presistance;
using Application.Common.Interfaces.RealTimeInterfaces;
using Application.Common.Interfaces.RealTimeInterfaces.PostInCourse;
using Application.Common.Interfaces.RealTimeInterfaces.PostInSection;
using Domain.Enums;
using Domain.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces.RealTimeInterfaces.PostInCourse;
using System.Runtime.InteropServices;

namespace Infrastructure.RealTimeServices
{
    public class PostCourseHub : Hub, IPostCourseHub
    {
        private readonly IUnitOfwork unitOfwork;
        private readonly ICheckDataOfRealTimeRequests checkDataOfRealTimeRequests;

        public PostCourseHub(IUnitOfwork unitOfwork, ICheckDataOfRealTimeRequests checkDataOfRealTimeRequests)
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
                var CourseIdString = Context?.GetHttpContext()?.Request.Query["CourseId"];

                if (userName is not null && !string.IsNullOrEmpty(CourseIdString))
                {
                    var TypeOfuserAndId = await checkDataOfRealTimeRequests.GetTypeOfUserAndHisId(userName);

                    if (TypeOfuserAndId is not null & int.TryParse(CourseIdString, out int CourseId))
                    {
                        bool IfUserInThisCourse = await checkDataOfRealTimeRequests.CheckIfUserInCourse(CourseId: CourseId,
                            UserId: TypeOfuserAndId.Item2.Id, typesOfUsers: TypeOfuserAndId.Item1);

                        // Add the user to the corresponding section group
                        if (IfUserInThisCourse)
                        {
                            await Groups.AddToGroupAsync(Context.ConnectionId, $"Course-{CourseId}");
                        }
                    }
                }
            }
            await base.OnConnectedAsync();
        }
        public async void AddPostInCourse(AddPostInCourseSenderMessage postMessage)
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
                    CourseCycleId = postMessage.CourseId,
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
                    await Clients.Group($"Course-{postMessage.CourseId}").SendAsync("AddCoursePost", postMessageforClinets);
                }
                catch (Exception ex)
                {
                }
            }


        }

        public async void DeletePostInCourse(DeletePostInCourseSenderMessage postMessage)
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

                        await Clients.Group($"Course-{postMessage.CourseId}").SendAsync("DeleteCoursePost", postReceiverMessage);
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }

        public async void UpdatePostInCourse(UpdatePostInCourseSenderMessage postMessage)
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

                        await Clients.Group($"Course-{postMessage.CourseId}").SendAsync("UpdateCoursePost", postReceiverMessage);
                    }
                }
                catch (Exception ex) { }

            }
        }
    }
}
