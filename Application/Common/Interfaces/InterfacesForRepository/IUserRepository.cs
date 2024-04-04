using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.InterfacesForRepository
{
	public interface IUserRepository
	{
		public Task<IEnumerable<User>> GetAllStudentsAsync();
		public Task<IEnumerable<User>> GetAllInstructorsAsync();
		public Task<IEnumerable<User>> GetAllProfessorsAsync();
	}
}
