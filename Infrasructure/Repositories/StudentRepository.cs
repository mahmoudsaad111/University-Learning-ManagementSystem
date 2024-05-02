
using Application.Common.Interfaces.InterfacesForRepository;
using Application.Common.Interfaces.Presistance;
using Contract.Dto.ReturnedDtos;
using Domain.Models;
using Infrastructure.Common;
using InfraStructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class StudentRepository : BaseRepository<Student>, IStudentRepository
	{
		public StudentRepository(AppDbContext appDbContext) : base(appDbContext)
		{ 			 	 

		}

        public async Task<IEnumerable<ReturnedStudentDto>> GetAllStudentsInDepartementAndAcadimicYear(  int AcadimicYearId)
        {
            try
            {
                var Students =await (from s in _appDbContext.Students
                                where  s.AcadimicYearId == AcadimicYearId
                                join u in _appDbContext.Users on s.StudentId equals u.Id
                                select new ReturnedStudentDto
                                {
                                    FirstName = u.FirstName,
                                    SecondName = u.SecondName,
                                    ThirdName = u.ThirdName,
                                    FourthName = u.FourthName,
                                    Address = u.Address,
                                    Email = u.Email,
                                    UserName = u.UserName,
                                    Gender = u.Gender,
                                    BirthDay = u.BirthDay,
                                    GPA = s.GPA,
                                    AcadimicYearId = s.AcadimicYearId,
                                    DepartementId = s.DepartementId,
                                    GroupId = s.GroupId
                                }).ToListAsync();
                return Students;
            }
            catch (Exception ex)  
            {
                return Enumerable.Empty<ReturnedStudentDto>();
            }
        }

        public async Task<int> GetStudentIdUsingUserName(string userName)
        {

            return await (from user in _appDbContext.Users where user.UserName == userName select user.Id).FirstOrDefaultAsync();
        }
        public async Task<bool> CheckIfStudentInGroup(int StudetId, int GroupId)
        {
            var Student = await _appDbContext.Students.FirstOrDefaultAsync(s => s.StudentId == StudetId && s.GroupId == GroupId);
            return StudetId != null;
        }
    }
}
