using Application.Common.Interfaces.CQRSInterfaces;
using Contract.Dto.PostReplies;
 

namespace Application.CQRS.Command.PostReplies
{
	public class DeletePostReplyCommand : ICommand<int>
	{
		public int Id { get; set; }	
	//	public PostReplyDto PostReplyDto { get; set; }
	}
}
