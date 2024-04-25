using Application.Common.Interfaces.InterfacesForRepository;
using Contract.Dto;
using Contract.Dto.Groups;
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
	public class GroupRepository : BaseRepository<Group>, IGroupRepository
	{
		public GroupRepository(AppDbContext _appDbContext) : base(_appDbContext)
		{
		}

        public async Task<AcadimicYear> GetAcadimicYearHasSpecificGroup(int groupId)
        {
            try
            {
                var Group = await _appDbContext.Groups.AsNoTracking().Include(g => g.AcadimicYear).FirstOrDefaultAsync(g => g.GroupId == groupId);
                if (Group is null)
                    return null;
                return Group.AcadimicYear;
            }
            catch (Exception ex)
            {
                return null; 
            }
        }

        public async Task<IEnumerable<GroupLessInfoDto>> GetGroupsOfDepartement(int DepartementId)
        {
            try
            {
                var GroupsWithAcadimicYear = await (from G in _appDbContext.Groups
                              join AC in _appDbContext.AcadimicYears on G.AcadimicYearId equals AC.AcadimicYearId
                              join D in _appDbContext.Departements on AC.DepartementId equals D.DepartementId
                              where D.DepartementId==DepartementId
                              select new GroupLessInfoDto
                              {
                                  GroupId = G.GroupId,
                                  Name = G.Name,
                                  NumberOfStudent = G.NumberOfStudent,
                                  StudentHeadName = G.StudentHeadName,
                                  StudentHeadPhone = G.StudentHeadPhone,
                                  Year = AC.Year,
                                  AcadimicYearId = AC.AcadimicYearId

                              }).ToListAsync();
                return GroupsWithAcadimicYear;
            }
            catch (Exception ex )
            {
                return new List<GroupLessInfoDto>();    
            }
        }

        public async Task<IEnumerable<NameIdDto>> GetLessInfoGroupsOfAcadimicYear(int AcadimicYearId)
        {
            try
            {
                return await _appDbContext.Groups.AsNoTracking().Where(g => g.AcadimicYearId == AcadimicYearId).Select(g => new NameIdDto
                {
                    Id = g.GroupId,
                    Name = g.Name
                }).ToListAsync();
            }
            catch(Exception ex)
            {
                return Enumerable.Empty<NameIdDto>();
            }
        }
    }
}
