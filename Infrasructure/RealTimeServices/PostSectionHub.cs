using Application.Common.Interfaces.Presistance;
using Application.Common.Interfaces.RealTimeInterfaces;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;
using System.Runtime.InteropServices;
using Application.Common.Interfaces.RealTimeInterfaces.PostInSection;
using Domain.Models;
using static System.Collections.Specialized.BitVector32;
using Application.Common.Interfaces.RealTimeInterfaces.PostReplyInSection;


namespace Infrastructure.RealTimeServices
{
    public class PostSectionHub : Hub, IPostSectionHub
    {
        private readonly IUnitOfwork unitOfwork;
        private readonly ICheckDataOfRealTimeRequests checkDataOfRealTimeRequests;

        public PostSectionHub(IUnitOfwork unitOfwork, ICheckDataOfRealTimeRequests checkDataOfRealTimeRequests)
        {
            this.unitOfwork = unitOfwork;
            this.checkDataOfRealTimeRequests = checkDataOfRealTimeRequests;
        }

        public async override Task OnConnectedAsync()
        {
            if (Context is not null)
            {
                string connectionId = Context.ConnectionId;

                // var userName = Context?.User?.Identity?.Name;
                var userName = Context?.GetHttpContext()?.Request.Query["UserName"];

                var sectionIdString = Context?.GetHttpContext()?.Request.Query["SectionId"];

                if (userName is not null && !string.IsNullOrEmpty(sectionIdString))
                {
                    var TypeOfuserAndId = await checkDataOfRealTimeRequests.GetTypeOfUserAndHisId(userName);

                    if (TypeOfuserAndId is not null & int.TryParse(sectionIdString, out int sectionId))
                    {
                        bool IfUserInThisSection = await checkDataOfRealTimeRequests.CheckIfUserInSection(SectionId: sectionId,
                            UserId: TypeOfuserAndId.Item2.Id, typesOfUsers: TypeOfuserAndId.Item1);

                        // Add the user to the corresponding section group
                        if (IfUserInThisSection)
                        {
                            await Groups.AddToGroupAsync(connectionId, $"Section-{sectionId}");
                        }
                    }
                }
            }
            await base.OnConnectedAsync();
        }

        public async Task AddPostInSection(PostAddSenderMessage postMessage)
        {
            var TypeOfuserAndId = await checkDataOfRealTimeRequests.GetTypeOfUserAndHisId(postMessage.SenderUserName);

            if (TypeOfuserAndId is not null && (TypeOfuserAndId.Item1 == TypesOfUsers.Professor
                || TypeOfuserAndId.Item1 == TypesOfUsers.Instructor))
            {

                User user = TypeOfuserAndId.Item2;

                var post = new Post
                {
                    Content = postMessage.PostContent,
                    CreatedBy = postMessage.SenderUserName,
                    IsProfessor = true ? TypeOfuserAndId.Item1 == TypesOfUsers.Professor : false,
                    SectionId = postMessage.SectionId,
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
                    await Clients.Group($"Section-{postMessage.SectionId}").SendAsync("AddSectionPost", postMessageforClinets);
                }
                catch (Exception ex)
                {

                }
            }

        }

        public async Task DeletePostInSection(PostDeleteSenderMessage postMessage)
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

                        await Clients.Group($"Section-{postMessage.SectionId}").SendAsync("DeleteSectionPost", postReceiverMessage);
                    }
                }
                catch (Exception ex)
                {

                }
            }

        }

        public async Task UpdatePostInSection(PostUpdateSenderMessage postMessage)
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
                        await unitOfwork.SaveChangesAsync();
                        PostUpdateReceiverMessage postReceiverMessage = new PostUpdateReceiverMessage
                        {
                            PostId = postMessage.PostId,
                            PostContent = postMessage.PostContent,
                        };

                        await Clients.Group($"Section-{postMessage.SectionId}").SendAsync("UpdateSectionPost", postReceiverMessage);
                    }
                }
                catch (Exception ex)
                {

                }
            }

        }

        public async Task AddPostReplyInSection(AddPostReplyInSectionSenderMessage postReplyMessage)
        {
            var TypeOfuserAndId = await checkDataOfRealTimeRequests.GetTypeOfUserAndHisId(postReplyMessage.SenderUserName);

            if (TypeOfuserAndId is not null)
            {

                User user = TypeOfuserAndId.Item2;

                var postReply = new PostReply
                {
                    Content = postReplyMessage.PostReplyContent,
                    CreatedBy = user.UserName,
                    CreatedAt = DateTime.Now,
                    ReplierId = user.Id,
                    PostId = postReplyMessage.PostId,
                };
                try
                {
                    var postReplyInDB = await unitOfwork.PostReplyRepository.CreateAsync(postReply);
                    await unitOfwork.SaveChangesAsync();
                    var postReplyMessageforClinets = new AddPostReplyInSectionReceiverMessage
                    {
                        SenderImageUrl = user.ImageUrl,
                        SenderName = user.FullName,
                        SenderUserName = user.UserName,
                        PostReplyContent = postReply.Content,
                        PostId = postReplyInDB.PostId,
                        PostReplyId = postReplyInDB.PostReplyId

                    };
                    await Clients.Group($"Section-{postReplyMessage.SectionId}").SendAsync("AddPostReplyInSection", postReplyMessageforClinets);
                }
                catch (Exception ex)
                {

                }
            }
        }

        public async Task DeletePostReplyInSection(DeletePostReplyInSectionSenderMessage postReplyMessage)
        {
            var TypeOfuserAndId = await checkDataOfRealTimeRequests.GetTypeOfUserAndHisId(postReplyMessage.SenderUserName);

            var postReply = await unitOfwork.PostReplyRepository.GetByIdAsync(postReplyMessage.PostReplyId);

            // If there is post with the given id and the publisher is the same who want to delete it then OK 

            if (postReply != null && (postReply.ReplierId == TypeOfuserAndId.Item2.Id || TypeOfuserAndId.Item1 == TypesOfUsers.Instructor))
            {
                try
                {
                    bool IsDeleted = await unitOfwork.PostReplyRepository.DeleteAsync(postReplyMessage.PostReplyId);
                    if (IsDeleted)
                    {
                        await unitOfwork.SaveChangesAsync();
                        var postReplyReceiverMessage = new DeletePostReplyInSectionReceiverMessage
                        {
                            PostId = postReplyMessage.PostId,
                            PostReplyId = postReplyMessage.PostReplyId,
                            SectionId = postReplyMessage.SectionId,
                        };

                        await Clients.Group($"Section-{postReplyMessage.SectionId}").SendAsync("DeleteSectionPost", postReplyReceiverMessage);
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }

        public async Task UpdatePostReplyInSection(UpdatePostReplyInSectionSenderMessage postReplyMessage)
        {
            var TypeOfuserAndId = await checkDataOfRealTimeRequests.GetTypeOfUserAndHisId(postReplyMessage.SenderUserName);

            var postReply = await unitOfwork.PostReplyRepository.GetByIdAsync(postReplyMessage.PostReplyId);

            // If there is post with the given id and the publisher is the same who want to delete it then OK 

            if (postReply != null && postReply.ReplierId == TypeOfuserAndId.Item2.Id)
            {
                postReply.Content = postReplyMessage.PostReplyContent;

                try
                {
                    bool IsUpdated = await unitOfwork.PostReplyRepository.UpdateAsync(postReply);

                    if (IsUpdated)
                    {
                        await unitOfwork.SaveChangesAsync();
                        var postReplyReceiverMessage = new UpdatePostReplyInSectionReceiverMessage
                        {
                            PostId = postReplyMessage.PostId,
                            PostReplyContent = postReplyMessage.PostReplyContent,
                            PostReplyId = postReplyMessage.PostReplyId,
                        };

                        await Clients.Group($"Section-{postReplyMessage.SectionId}").SendAsync("UpdateSectionPost", postReplyReceiverMessage);
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }
    }
}

// Last work ; 
/*
    public override async Task OnConnectedAsync()
        {
            string ConnectionId = Context.ConnectionId;
            var userName = Context?.User?.Identity?.Name;
            var role = "df";
            //var userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userName is not null)
            {
                var user =await unitOfwork.UserRepository.GetUserByUserName(userName);         
                if (user is not null)
                {
                    IEnumerable<int> SectionsId = null; 
                    if(user.Student is not null)
                    {
                        int StudentId = user.Student.StudentId;  
                        SectionsId = await unitOfwork.StudentSectionRepository.GetAllSectionsIdofStudent(StudentId);          
                    }

                    else if(user.Instructor is not null)
                    {
                        int InstrctorId = user.Instructor.InstructorId;
                        SectionsId=await unitOfwork.SectionRepository.GetAllSectionsIdOfInstructore(InstrctorId);
                    }
                    else if(user.Professor is not null)
                    {
                        int ProfessorId = user.Professor.ProfessorId;
                        SectionsId =await unitOfwork.SectionRepository.GetAllSectionsIdOfProfessor(ProfessorId);
                    }

                    foreach (var sectionId in SectionsId)
                    {
                        await Groups.AddToGroupAsync(ConnectionId, $"Section{sectionId}");
                    }
                }
            }

            await base.OnConnectedAsync();
        }
 */