using Application.Common.Interfaces.InterfacesForRepository;
using Application.Common.Interfaces.Presistance;
using Contract.Dto;
using Contract.Dto.ReturnedDtos;
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
    public class ProfessorRepository : BaseRepository<Professor> , IProfessorRepository
	{	 
		public ProfessorRepository(AppDbContext appDbContext) : base(appDbContext)
		{
			 
		}

        public async Task<IEnumerable<ReturnedProfessorDto>> GetAllProfessorsInDepartement(int DeptId)
        {
            try
            {
                var Professors = await (from p in _appDbContext.Professors
                                     where p.DepartementId == DeptId 
                                     join u in _appDbContext.Users on p.ProfessorId equals u.Id 
                                     select new ReturnedProfessorDto
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
                                         Specification=p.Specification,
                                         DepartementId=p.DepartementId
                                         
                                     }).ToListAsync();
                return Professors;
            }
            catch (Exception ex)
            {
                return Enumerable.Empty<ReturnedProfessorDto>();
            }
        }

        public async Task<bool> CheckIfProfessorInSection(int ProfessorId, int SectionId)
        {
            int SectionIdFromQuery = await (
                                                from p in _appDbContext.Professors
                                                where p.ProfessorId == ProfessorId
                                                join cc in _appDbContext.CourseCycles on p.ProfessorId equals cc.ProfessorId
                                                join sec in _appDbContext.Sections on cc.CourseCycleId equals sec.CourseCycleId
                                                where sec.SectionId == SectionId
                                                select SectionId
                       ).FirstAsync();

            return (SectionIdFromQuery == SectionId);
        }

        public async Task<IEnumerable<NameIdDto>> GetLessInfoProfessorByDeptId(int DeptId)
        {
            return await (from prof in _appDbContext.Professors
                          where prof.DepartementId == DeptId
                          join user in _appDbContext.Users on prof.ProfessorId equals user.Id
                          select new NameIdDto
                          {
                              Id = user.Id,
                              Name = $"{user.FirstName} {user.SecondName}"
                          }).ToListAsync();
        }
    }
}
