using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Dto.ReturnedDtos
{
	public class ReturnedStudentDto : ReturnedUserDto
	{
		public double GPA { get; set; }
		public ushort AcadimicYear { get; set; }
		public int DepartementId { get; set; }
		public int GroupId { get; set; }
	}
}
