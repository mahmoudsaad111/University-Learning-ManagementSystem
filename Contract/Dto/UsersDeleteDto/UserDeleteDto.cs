using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Dto.UsersDeleteDto
{
	public class UserDeleteDto
	{
		[Required]
		[EmailAddress]
		public string Email { get; set; }
	}
}
