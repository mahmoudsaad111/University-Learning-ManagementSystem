using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Dto.Groups
{
	public class GroupDto
	{
		public string Name { get; set; }
		public string StudentHeadName { get; set; } = null!;
		public string StudentHeadPhone { get; set; } = null!;
		public short NumberOfStudent { get; set; }
		public int AcadimicYear { get; set; }
		public int DepartementId { get; set; }
		public Group GetGroup()
		{
			return new Group
			{
				Name = Name,
				StudentHeadName = StudentHeadName,
				StudentHeadPhone = StudentHeadPhone,
				NumberOfStudent = NumberOfStudent,
				AcadimicYearId = AcadimicYear 
			};
		}
	}
}
