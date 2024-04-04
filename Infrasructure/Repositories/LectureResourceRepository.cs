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
    public class LectureResourceRepository : BaseRepository<LectureResource> , ILectureResourceRepository
    {
        public LectureResourceRepository(AppDbContext _appDbContext) : base(_appDbContext)
        {
        }

        public async Task<IEnumerable<string>> GetAllFilesUrlForLectureAsync(int LectureId)
        {
            try
            {
                return await _appDbContext.LectureResources.Where(lr => lr.LectureId == LectureId).Select(lr => lr.Url).ToListAsync();
            }
            catch (Exception ex)
            {
                return new List<string>();
            }
        }
    }
}
