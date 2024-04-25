using Application.Common.Interfaces.CQRSInterfaces;
using Contract.Dto.ReturnedDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Query.Instructors
{
	public class GetAllInstructorsQuery : IQuery<List<ReturnedInstructorDto>>
	{
		public int Id { get; set; }
	}
}
