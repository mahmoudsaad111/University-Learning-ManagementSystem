using Application.Common.Interfaces.CQRSInterfaces;
using Contract.Dto.ReturnedDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Query.Professors
{
	public class GetAllProfessorsQuery : IQuery<List<ReturnedProfessorDto>>
	{

	}
}
