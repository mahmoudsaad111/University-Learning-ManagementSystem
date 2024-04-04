using Domain.Models;
using Infrastructure.Common;
using InfraStructure;

namespace Infrastructure.Repositories
{
    public class CommentReplyRepository : BaseRepository<CommentReply>
    {
        public CommentReplyRepository(AppDbContext _appDbContext) : base(_appDbContext)
        {
        }
    }
}
