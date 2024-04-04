using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Dto.ReturnedDtos
{
	public class ReturnedProfessorDto :ReturnedUserDto
	{
		public string Specification { get; set; } 
		public int? DepartementId { get; set; }
	}
}
