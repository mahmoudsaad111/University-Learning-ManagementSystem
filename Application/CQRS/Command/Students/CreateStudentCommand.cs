using Application.Common.Interfaces.CQRSInterfaces;
using Contract.Dto.UsersRegisterDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Command.Students
{
	public class CreateStudentCommand : ICommand<StudentRegisterDto>

	{
		public StudentRegisterDto StudentRegisterDto { get; set; }
	}
}
