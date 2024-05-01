using Application.Common.Interfaces.InterfacesForRepository;
using Domain.Enums;
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
    public class ExamPlaceRepository : BaseRepository<ExamPlace>, IExamPlaceRepository
    {
        public ExamPlaceRepository(AppDbContext _appDbContext) : base(_appDbContext)
        {
        }
    }
}
