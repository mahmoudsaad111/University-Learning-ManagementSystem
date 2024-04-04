using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Dto.UserUpdatedDto
{
	public class ProfessorUpdatedDto :UserUpdatedDto
	{
		public string? Specification { get; set; }
		public int DepartementId { get; set; }

		public Professor GetProfessor()
		{
			return new Professor
			{
				ProfessorId=Id,
				DepartementId = DepartementId,
				Specification = Specification
			};
		}
	}
}
