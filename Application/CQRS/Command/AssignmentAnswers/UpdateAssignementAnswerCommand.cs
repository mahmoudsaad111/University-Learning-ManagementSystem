using Application.Common.Interfaces.CQRSInterfaces;
using Contract.Dto.AssignementAnswers;
 
using Domain.Models;
 

namespace Application.CQRS.Command.AssignementAnswers
{
	public class UpdateAssignementAnswerCommand  : ICommand<AssignmentAnswer>
	{
		public int Id { get; set; }
		public AssignementAnswerDto AssignementAnswerDto { get; set; }
	}
}
