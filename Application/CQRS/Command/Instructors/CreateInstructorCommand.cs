using Application.Common.Interfaces.CQRSInterfaces;
using Contract.Dto.UsersRegisterDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Command.Instructors
{
	public class CreateInstructorCommand : ICommand<InstructorRegisterDto>
	{
		public InstructorRegisterDto InstructorRegisterDto { get; set; }
	}
}
