using Application.Common.Interfaces.CQRSInterfaces;
using Contract.Dto.AssignementAnswers;
 

namespace Application.CQRS.Command.AssignementAnswers
{
	public class DeleteAssignementAnswerCommand : ICommand<int>
	{
		public int Id { get; set; }	
		//public AssignementAnswerDto AssignementAnswerDto { get; set; }
	}
}
