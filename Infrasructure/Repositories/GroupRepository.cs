using Application.Common.Interfaces.InterfacesForRepository;
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
    }
}
