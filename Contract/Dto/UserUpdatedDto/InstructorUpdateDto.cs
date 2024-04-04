using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Dto.UserUpdatedDto
{
	public class InstructorUpdateDto :UserUpdatedDto
	{
		public string? Specification { get; set; }
		public int DepartementId { get; set; }

		public Instructor GetInstructor()
		{
			return new Instructor
			{
				InstructorId=Id,
				DepartementId = DepartementId,
				Specification = Specification
			};
		}
	}
}
