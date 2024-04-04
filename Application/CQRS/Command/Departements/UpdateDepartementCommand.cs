using Application.Common.Interfaces.CQRSInterfaces;
using Contract.Dto.Departements;
 
using Domain.Models;
 

namespace Application.CQRS.Command.Departements
{
	public class UpdateDepartementCommand  : ICommand<Departement>
	{
		public int Id { get; set; }
		public DepartementDto departementDto { get; set; }
	}
}
