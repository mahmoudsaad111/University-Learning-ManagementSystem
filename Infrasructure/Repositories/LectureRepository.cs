using Domain.Models;
using Infrastructure.Common;
using InfraStructure;

namespace Infrastructure.Repositories
{
    public class LectureRepository : BaseRepository<Lecture>
    {
        public LectureRepository(AppDbContext _appDbContext) : base(_appDbContext)
        {
        }
    }
}
