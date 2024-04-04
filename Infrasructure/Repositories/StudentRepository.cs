
using Application.Common.Interfaces.Presistance;
using Domain.Models;
using Infrastructure.Common;
using InfraStructure;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class StudentRepository : BaseRepository<Student>
	{
		public StudentRepository(AppDbContext appDbContext) : base(appDbContext)
		{ 			 	 

		}
	}
}
