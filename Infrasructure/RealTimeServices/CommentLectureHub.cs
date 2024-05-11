using Application.Common.Interfaces.Presistance;
using Application.Common.Interfaces.RealTimeInterfaces;
using Application.Common.Interfaces.RealTimeInterfaces.CommentInLecture;
using Application.Common.Interfaces.RealTimeInterfaces.CommentReplyInLecture;
using Application.Common.Interfaces.RealTimeInterfaces.PostInSection;
using Application.Common.Interfaces.RealTimeInterfaces.PostReplyInCourseCycle;
using Domain.Enums;
using Domain.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.RealTimeServices
{
    public class CommentLectureHub : Hub, ICommentLectureHub
    {
        private readonly IUnitOfwork unitOfwork;
        private readonly ICheckDataOfRealTimeRequests checkDataOfRealTimeRequests;

        public CommentLectureHub(IUnitOfwork unitOfwork, ICheckDataOfRealTimeRequests checkDataOfRealTimeRequests)
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
                var LectureIdString = Context?.GetHttpContext()?.Request.Query["LectureId"];
                var CourseCycleIdString = Context?.GetHttpContext()?.Request.Query["CourseCycleId"];
                var SectionIdString = Context?.GetHttpContext()?.Request.Query["SectionId"];

                if (userName is not null && (!string.IsNullOrEmpty(LectureIdString))
                    && ((!string.IsNullOrEmpty(CourseCycleIdString) || (!string.IsNullOrEmpty(SectionIdString)))))
                {
                    var TypeOfuserAndId = await checkDataOfRealTimeRequests.GetTypeOfUserAndHisId(userName);

                    bool IfUserInThisLecture = false;
                    if (TypeOfuserAndId is not null)
                    {
                        if (CourseCycleIdString is not null && (int.TryParse(CourseCycleIdString, out int CourseCycleId)))
                            IfUserInThisLecture = await checkDataOfRealTimeRequests.CheckIfUserInCourseCycle(CourseCycleId: CourseCycleId,
                             UserId: TypeOfuserAndId.Item2.Id, typesOfUsers: TypeOfuserAndId.Item1);
                        else if (SectionIdString is not null && (int.TryParse(SectionIdString, out int SectionId)))
                            IfUserInThisLecture = await checkDataOfRealTimeRequests.CheckIfUserInSection(SectionId: SectionId,
                               UserId: TypeOfuserAndId.Item2.Id, typesOfUsers: TypeOfuserAndId.Item1);
                    }

                    if (int.TryParse(LectureIdString, out int LectureId) && IfUserInThisLecture)
                    {
                        // Add the user to the corresponding section group
                        if (IfUserInThisLecture)
                        {
                            await Groups.AddToGroupAsync(Context.ConnectionId, $"Lecture-{LectureId}");
                        }
                    }
                }
            }
            await base.OnConnectedAsync();
        }
        public async Task AddCommentInLecture(AddCommentInLectureSenderMessage commentMessage)
        {
            var TypeOfuserAndId = await checkDataOfRealTimeRequests.GetTypeOfUserAndHisId(commentMessage.SenderUserName);

            if (TypeOfuserAndId is not null)
            {

                User user = TypeOfuserAndId.Item2;

                var comment = new Comment
                {
                    Content = commentMessage.CommentContent,
                    CreatedBy = commentMessage.SenderUserName,
                    UserId = user.Id,
                    CreatedAt = DateTime.UtcNow,
                    LectureId = commentMessage.LectureId,

                };
                try
                {
                    var CommentInDB = await unitOfwork.CommentRepository.CreateAsync(comment);
                    await unitOfwork.SaveChangesAsync();
                    var CommentMessageforClinets = new AddCommentInLectureReceiverMessage
                    {
                        SenderImageUrl = user.ImageUrl,
                        SenderName = user.FullName,
                        SenderUserName = user.UserName,
                        CommentContent = commentMessage.CommentContent,
                        CommentId = CommentInDB.CommentId

                    };
                    await Clients.Group($"Lecture-{commentMessage.LectureId}").SendAsync("AddCommentInLecture", CommentMessageforClinets);
                }
                catch (Exception ex)
                {
                }
            }
        }
        public async Task AddCommentReplyInLecture(AddCommentReplyInLectureSenderMessage commentReplyMessage)
        {
            var TypeOfuserAndId = await checkDataOfRealTimeRequests.GetTypeOfUserAndHisId(commentReplyMessage.SenderUserName);

            if (TypeOfuserAndId is not null)
            {

                User user = TypeOfuserAndId.Item2;

                var CommentReply = new CommentReply
                {
                    Content = commentReplyMessage.CommentReplyContent,
                    CreatedBy = commentReplyMessage.SenderUserName,
                    CreatedAt = DateTime.Now,
                    UserId = user.Id,
                    CommentId = commentReplyMessage.CommentId,

                };
                try
                {
                    var commentReplyInDB = await unitOfwork.CommentReplyRepository.CreateAsync(CommentReply);
                    await unitOfwork.SaveChangesAsync();
                    var commentReplyMessageforClinets = new AddCommentReplyInLectureReceiverMessage
                    {
                        SenderImageUrl = user.ImageUrl,
                        SenderName = user.FullName,
                        CommentReplyId = commentReplyInDB.CommentReplyId,
                        CommentReplyContent = commentReplyMessage.CommentReplyContent,
                        SenderUserName = user.UserName,
                        CommentId = commentReplyInDB.CommentId,

                    };
                    await Clients.Group($"Lecture-{commentReplyMessage.LectureId}").SendAsync("AddPCommentReplyInLecture", commentReplyMessageforClinets);
                }
                catch (Exception ex)
                {
                }
            }
        }
        public async Task DeleteCommentInLecture(DeleteCommentInLectureSenderMessage commentMessage)
        {
            var TypeOfuserAndId = await checkDataOfRealTimeRequests.GetTypeOfUserAndHisId(commentMessage.SenderUserName);

            var comment = await unitOfwork.CommentRepository.GetByIdAsync(commentMessage.CommentId);

            // If there is post with the given id and the publisher is the same who want to delete it then OK 

            if (comment != null && (comment.UserId == TypeOfuserAndId.Item2.Id ||
                (TypeOfuserAndId.Item1 == TypesOfUsers.Professor || TypeOfuserAndId.Item1 == TypesOfUsers.Instructor)))
            {
                try
                {
                    bool IsDeleted = await unitOfwork.CommentRepository.DeleteAsync(commentMessage.CommentId);
                    if (IsDeleted)
                    {
                        await unitOfwork.SaveChangesAsync();

                        var commentReceiverMessage = new DeleteCommentInLectureReceiverMessage
                        {
                            CommentId = commentMessage.CommentId,
                        };

                        await Clients.Group($"Lecture-{commentMessage.LectureId}").SendAsync("DeleteCommentInLecture", commentReceiverMessage);
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }
        public async Task DeleteCommentReplyInLecture(DeleteCommentReplyInLectureSenderMessage commentReplyMessage)
        {
            var TypeOfuserAndId = await checkDataOfRealTimeRequests.GetTypeOfUserAndHisId(commentReplyMessage.SenderUserName);

            var commentReply = await unitOfwork.CommentReplyRepository.GetByIdAsync(commentReplyMessage.CommentReplyId);

            // If there is post with the given id and the publisher is the same who want to delete it then OK 

            if (commentReply != null && (commentReply.UserId == TypeOfuserAndId.Item2.Id ||
                TypeOfuserAndId.Item1 == TypesOfUsers.Professor || TypeOfuserAndId.Item1 == TypesOfUsers.Instructor))
            {
                try
                {
                    bool IsDeleted = await unitOfwork.CommentReplyRepository.DeleteAsync(commentReplyMessage.CommentReplyId);
                    if (IsDeleted)
                    {
                        await unitOfwork.SaveChangesAsync();

                        var commentReplyReceiverMessage = new DeleteCommentReplyInLectureReceiverMessage
                        {
                            CommentId = commentReplyMessage.CommentId,
                            CommentReplyId = commentReplyMessage.CommentReplyId,
                        };

                        await Clients.Group($"Lecture-{commentReplyMessage.LectureId}").SendAsync("DeleteCommentReplyInLecture", commentReplyReceiverMessage);
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }
        public async Task UpdateCommentInLecture(UpdateCommentInLectureSenderMessage commentMessage)
        {
            var TypeOfuserAndId = await checkDataOfRealTimeRequests.GetTypeOfUserAndHisId(commentMessage.SenderUserName);

            var comment = await unitOfwork.CommentRepository.GetByIdAsync(commentMessage.CommentId);

            // If there is post with the given id and the publisher is the same who want to delete it then OK 

            if (comment != null && comment.UserId == TypeOfuserAndId.Item2.Id)
            {
                comment.Content = commentMessage.CommentContent;
                try
                {
                    bool IsUpdated = await unitOfwork.CommentRepository.UpdateAsync(comment);
                    if (IsUpdated)
                    {
                        await unitOfwork.SaveChangesAsync();

                        var commentReceiverMessage = new UpdateCommentInLectureReceiverMessage
                        {
                            CommentContent = commentMessage.CommentContent,
                            CommenntId = commentMessage.CommentId,
                        };

                        await Clients.Group($"Lecture-{commentMessage.LectureId}").SendAsync("UpdateCommentInLecture", commentReceiverMessage);
                    }
                }
                catch (Exception ex) { }

            }
        }
        public async Task UpdateCommentReplyInLecture(UpdateCommentReplyInLectureSenderMessage commentReplyMessage)
        {
            var TypeOfuserAndId = await checkDataOfRealTimeRequests.GetTypeOfUserAndHisId(commentReplyMessage.SenderUserName);

            var commentReply = await unitOfwork.CommentReplyRepository.GetByIdAsync(commentReplyMessage.CommentReplyId);

            if (commentReply != null && commentReply.UserId == TypeOfuserAndId.Item2.Id)
            {
                commentReply.Content = commentReplyMessage.CommentReplyContent;
                try
                {
                    bool IsUpdated = await unitOfwork.CommentReplyRepository.UpdateAsync(commentReply);

                    if (IsUpdated)
                    {
                        await unitOfwork.SaveChangesAsync();

                        var commentReplyReceiverMessage = new UpdateCommentReplyInLectureReceiverMessage
                        {
                            CommentId = commentReplyMessage.CommentId,
                            CommentReplyContent = commentReplyMessage.CommentReplyContent,
                            CommentReplyId = commentReplyMessage.CommentReplyId,
                        };

                        await Clients.Group($"Lecture-{commentReplyMessage.LectureId}").SendAsync("UpdateCommentReplyInLecture", commentReplyReceiverMessage);
                    }
                }
                catch (Exception ex) { }

            }
        }
    }
}
