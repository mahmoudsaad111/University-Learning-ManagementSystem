using Application.Common.Interfaces.CQRSInterfaces;
using Contract.Dto.Assignements;
 
using Domain.Models;
 

namespace Application.CQRS.Command.Assignements
{
	public class UpdateAssignementCommand  : ICommand<Assignment>
	{
		public int Id { get; set; }
		public AssignementDto AssignementDto { get; set; }
	}
}
