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
    public class SectionRepository : BaseRepository<Section>, ISectionRepository
	{
		public SectionRepository(AppDbContext _appDbContext) : base(_appDbContext)
		{
		}

        public async Task<Section> GetSectionHasSpecificLectureUsingLectureIdAsync(int LectureId)
        {
                var Section = await _appDbContext.Sections.FirstOrDefaultAsync(s => s.Lectures.Any(l => l.LectureId == LectureId));
                return Section; 
        }
    }
}
