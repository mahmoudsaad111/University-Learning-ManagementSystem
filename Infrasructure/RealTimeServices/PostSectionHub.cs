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

namespace Infrastructure.RealTimeServices
{
    public class PostSectionHub : Hub<IPostSectionInClient>  , IPostSectionHub
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
                var userName = Context?.User?.Identity?.Name;
                var sectionIdString = Context?.GetHttpContext()?.Request.Query["sectionId"];

                if (userName is not null)
                {
                    var TypeOfuserAndId = await checkDataOfRealTimeRequests.GetTypeOfUserAndHisId(userName);

                    if (TypeOfuserAndId is not null && !string.IsNullOrEmpty(sectionIdString) && int.TryParse(sectionIdString, out int sectionId))
                    {
                        bool IfUserInThisSection = await checkDataOfRealTimeRequests.CheckIfUserInSection(SectionId: sectionId,
                            UserId: TypeOfuserAndId.Item2, typesOfUsers: TypeOfuserAndId.Item1);

                        // Add the user to the corresponding section group
                        if (IfUserInThisSection)
                        {
                            await Groups.AddToGroupAsync(Context.ConnectionId, $"Section-{sectionId}");
                        }
                    }
                }
            }
            await base.OnConnectedAsync();
        }

        public void AddPostInSection(int UserId, int SectionId, string PostContent)
        {
            throw new NotImplementedException();
        }

        public void DeletePostInSection(int UserId, int SectionId, int PostId)
        {
            throw new NotImplementedException();
        }

        public void UpdatePostInSection(int UserId, int SectionId, int PostId, string PostContent)
        {
            throw new NotImplementedException();
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