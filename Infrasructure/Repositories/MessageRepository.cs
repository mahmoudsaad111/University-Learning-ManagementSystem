using Application.Common.Interfaces.InterfacesForRepository;
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
    public class MessageRepository : BaseRepository<Message>
    {
        public MessageRepository(AppDbContext _appDbContext) : base(_appDbContext)
        {
        }
    }
}