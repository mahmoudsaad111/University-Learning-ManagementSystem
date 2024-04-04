using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Dto.Departements
{
	public class DepartementDto
	{
		[Required]
		public string Name { get; set; } = null!;
		[Required]
		[Phone] 
		public string StudentServiceNumber { get; set; } = null!;
		[Required]
		public string ProfHeadName { get; set; } = null!;
		[Required]
		public int FacultyId { get; set; }

		public Departement GetDepartement()
		{
			return new Departement
			{
				Name = Name,
				StudentServiceNumber = StudentServiceNumber,
				ProfHeadName = ProfHeadName,
				FacultyId = FacultyId == 0 ? 1 : FacultyId
			};
		}
	}
}
