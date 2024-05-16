using Application.Common.Interfaces.InterfacesForRepository;
using Contract.Dto.AssignementAnswers;
using Contract.Dto.Assignements;
using Domain.Models;
using Infrastructure.Common;
using InfraStructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class AssignementAnswerRepository : BaseRepository<AssignmentAnswer> , IAssignementAnswerRepository
    {
        public AssignementAnswerRepository(AppDbContext _appDbContext) : base(_appDbContext)
        {
        }

        public async Task<IEnumerable<StudentHasAnswerOfAssignemet>> GetAllStudentWithAnswersOnAssignemnt(int assignemntId)
        {
            var Result = await (from aa in _appDbContext.AssignmentAnswers
                                where aa.AssignmentId == assignemntId
                                from u in _appDbContext.Users
                                where u.Id == aa.StudentId
                                select new StudentHasAnswerOfAssignemet
                                {

                                    StudentId = aa.StudentId,
                                    FirstName = u.FirstName,
                                    SecondName = u.SecondName,
                                    ThirdName = u.ThirdName,
                                    FourthName = u.FourthName,
                                    Email = u.Email,
                                    UserName = u.UserName,
                                    ImageUrl = u.ImageUrl,
                                    StudentMarks = aa.Mark,
                                    StudentAnswerFiles = (from fr in _appDbContext.AssignmentAnswerResources
                                                          where fr.AssignmentAnswerId == aa.AssignmentAnswer_id
                                                          select new AssignmentAnswerResource
                                                          {
                                                              Url = fr.Url,
                                                              AssignmentAnswerId = fr.AssignmentAnswerId,
                                                              CreatedAt = fr.CreatedAt ,
                                                              Name = fr.Name,
                                                              FileType = fr.FileType,
                                                          }).ToList()
                                }
                                ).ToListAsync();
            return Result;  
        }

    }
}
