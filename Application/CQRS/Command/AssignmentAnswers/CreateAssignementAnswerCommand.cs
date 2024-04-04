using Application.Common.Interfaces.CQRSInterfaces;
using Contract.Dto.AssignementAnswers;
using Domain.Models;


namespace Application.CQRS.Command.AssignementAnswers
{
	public class CreateAssignementAnswerCommand : ICommand<AssignmentAnswer>
	{
		public AssignementAnswerDto AssignementAnswerDto { get; set; }
	}
}
