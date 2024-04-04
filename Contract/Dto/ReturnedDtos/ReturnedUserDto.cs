using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Dto.ReturnedDtos
{
	public class ReturnedUserDto
	{
		public string FirstName { get; set; } = null!;
		public string SecondName { get; set; } = null!;
		public string ThirdName { get; set; } = null!;
		public string FourthName { get; set; } = null!;
		public string Address { get; set; } = null!;
		public string Email {  get; set; } = null!;
		public string UserName { get; set; } = null!;
		public bool Gender { get; set; }
		public DateTime BirthDay { get; set; } = DateTime.MinValue;
	}
}
