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

        public async Task<IEnumerable<int>> GetAllSectionsIdOfInstructore(int instructorId)
        {
            return await _appDbContext.Sections.AsNoTracking().Where(s => s.InstructorId == instructorId).Select(s => s.SectionId).ToListAsync();
        }

        public async Task<IEnumerable<int>> GetAllSectionsIdOfProfessor(int ProfesssorId)
        {
            return await (
                            from P in _appDbContext.Professors
                            join CC in _appDbContext.CourseCycles on P.ProfessorId equals CC.ProfessorId
                            join S in _appDbContext.Sections on CC.CourseCycleId equals S.CourseCycleId
                            select S.SectionId
                            
                          ).ToListAsync();                           
        }

        public async Task<Section> GetSectionHasSpecificLectureUsingLectureIdAsync(int LectureId)
        {
                var Section = await _appDbContext.Sections.FirstOrDefaultAsync(s => s.Lectures.Any(l => l.LectureId == LectureId));
                return Section; 
        }
    }
}
