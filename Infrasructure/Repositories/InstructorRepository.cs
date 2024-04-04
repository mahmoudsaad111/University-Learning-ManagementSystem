using Domain.Models;
 
using InfraStructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces.Presistance;
using Infrastructure.Common;

namespace Infrastructure.Repositories
{
    public class InstructorRepository : BaseRepository<Instructor>
	{
		public InstructorRepository(AppDbContext appDbContext) : base( appDbContext)
		{
		}
	}
}
