using Application.Common.Interfaces.CQRSInterfaces;
using Contract.Dto.UsersRegisterDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Command.Professors
{
	public class CreateProfessorCommand : ICommand<ProfessorRegisterDto>
	{
		public ProfessorRegisterDto ProfessorRegisterDto { get; set; }
	}
}
