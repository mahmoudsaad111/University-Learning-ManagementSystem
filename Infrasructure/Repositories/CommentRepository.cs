using Domain.Models;
using Infrastructure.Common;
using InfraStructure;

namespace Infrastructure.Repositories
{
    public class CommentRepository : BaseRepository<Comment>
    {
        public CommentRepository(AppDbContext _appDbContext) : base(_appDbContext)
        {
        }
    }
}
