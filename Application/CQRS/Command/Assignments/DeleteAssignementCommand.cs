using Application.Common.Interfaces.CQRSInterfaces;
using Contract.Dto.Assignements;
 

namespace Application.CQRS.Command.Assignements
{
	public class DeleteAssignementCommand : ICommand<int>
	{
		public int Id { get; set; }	
		public AssignementDto AssignementDto { get; set; }
	}
}
