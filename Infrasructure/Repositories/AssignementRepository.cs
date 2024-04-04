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
    public class AssignementRepository : BaseRepository<Assignment>
    {
        public AssignementRepository(AppDbContext _appDbContext) : base(_appDbContext)
        {
        }
    }
}
