using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Dto.ReturnedDtos
{
	public class ReturnedInstructorDto :ReturnedUserDto
	{
		public string Specification { get; set; } = null!;
		public int? DepartementId { get; set; }
	}
}
