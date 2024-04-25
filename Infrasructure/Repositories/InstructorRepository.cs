using Domain.Models;
 
using InfraStructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces.Presistance;
using Infrastructure.Common;
using Application.Common.Interfaces.InterfacesForRepository;
using Contract.Dto.ReturnedDtos;
using Microsoft.EntityFrameworkCore;
using Contract.Dto;

namespace Infrastructure.Repositories
{
    public class InstructorRepository : BaseRepository<Instructor> , IInstructorRepository
	{
		public InstructorRepository(AppDbContext appDbContext) : base( appDbContext)
		{
		}
        public async Task<IEnumerable<NameIdDto>> GetLessInfoInstructorByDeptId(int DeptId)
        {
            return await (from inst in _appDbContext.Instructors
                          where inst.DepartementId == DeptId
                          join user in _appDbContext.Users on inst.InstructorId equals user.Id
                          select new NameIdDto
                          {
                              Id = user.Id,
                              Name = $"{user.FirstName} {user.SecondName} {user.ThirdName}"
                          }).ToListAsync();
        }
        public async Task<IEnumerable<ReturnedInstructorDto>> GetAllInstructorInDepartement(int DeptId)
        {
            try
            {
                var Instructors = await(from i in _appDbContext.Instructors
                                       where i.DepartementId == DeptId
                                       join u in _appDbContext.Users on i.InstructorId equals u.Id
                                       select new ReturnedInstructorDto
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
                                           Specification = i.Specification,
                                           DepartementId = i.DepartementId

                                       }).ToListAsync();
                return Instructors;
            }
            catch (Exception ex)
            {
                return Enumerable.Empty<ReturnedInstructorDto>();
            }
        }
    }
}
