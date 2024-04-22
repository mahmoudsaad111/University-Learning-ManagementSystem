using Application.Common.Interfaces.Presistance;
using Application.Common.Interfaces.RealTimeInterfaces;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.RealTimeServices
{
    public class PostSectionHub : Hub<IPostSectionHub> 
    {
        private readonly IUnitOfwork unitOfwork;

        public PostSectionHub(IUnitOfwork unitOfwork)
        {
            this.unitOfwork = unitOfwork;
        }

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
