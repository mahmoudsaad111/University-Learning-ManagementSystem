using Application.Common.Interfaces.CQRSInterfaces;
using Contract.Dto.UserUpdatedDto;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Command.Students
{
	public class UpdateStudentByIdCommand :ICommand
	{
		public int Id { get; set; }
		public StudentUpdatedDto NewStudentInfo { get; set; }
	}
}
