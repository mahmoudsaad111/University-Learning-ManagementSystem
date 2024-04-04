using Domain.Models;
using Infrastructure.Common;
using InfraStructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class PostRepository : BaseRepository<Post>
    {
        public PostRepository(AppDbContext _appDbContext) : base(_appDbContext)
        {
        }
    }
}
