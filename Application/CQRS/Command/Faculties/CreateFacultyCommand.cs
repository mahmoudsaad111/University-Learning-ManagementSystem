using Application.Common.Interfaces.CQRSInterfaces;
using Contract.Dto.Faculties;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Command.Faculties
{
    public class CreateFacultyCommand : ICommand<Faculty>
	{
		public FacultyDto FacultyDto { get; set; }
	}
}
