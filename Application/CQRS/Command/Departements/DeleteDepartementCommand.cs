using Application.Common.Interfaces.CQRSInterfaces;
using Contract.Dto.Departements;
 

namespace Application.CQRS.Command.Departements
{
	public class DeleteDepartementCommand : ICommand<int>
	{
		public int Id { get; set; }	
		//public DepartementDto DepartementDto { get; set; }
	}
}
