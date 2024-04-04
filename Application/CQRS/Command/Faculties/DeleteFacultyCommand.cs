using Application.Common.Interfaces.CQRSInterfaces;
using Contract.Dto.Faculties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ICommand = Application.Common.Interfaces.CQRSInterfaces.ICommand;

namespace Application.CQRS.Command.Faculties
{
	public class DeleteFacultyCommand : ICommand<int>
	{
		public int Id { get; set; }	
		public FacultyDto FacultyDto { get; set; }
	}
}
