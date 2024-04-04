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
	public class UserRepository : BaseRepository<User>, IUserRepository
	{
		public UserRepository(AppDbContext _appDbContext) : base(_appDbContext)
		{
		}

		public async Task<IEnumerable<User>> GetAllInstructorsAsync()
		{
			return await _appDbContext.Users.AsNoTracking().Where(user => user.Instructor != null).Include("Instructor").ToListAsync();

		}

		public async Task<IEnumerable<User>> GetAllProfessorsAsync()
		{
			return await _appDbContext.Users.AsNoTracking().Where(user => user.Professor != null).Include("Professor").ToListAsync();
			;
		}

		public async Task<IEnumerable<User>> GetAllStudentsAsync()
		{
			return  await _appDbContext.Users.AsNoTracking().Where(user=>user.Student!=null).Include("Student").ToListAsync();
		}
		 
	}
}
