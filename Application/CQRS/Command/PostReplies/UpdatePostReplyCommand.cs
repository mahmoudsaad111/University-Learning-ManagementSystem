using Application.Common.Interfaces.CQRSInterfaces;
using Contract.Dto.PostReplies;
 
using Domain.Models;
 

namespace Application.CQRS.Command.PostReplies
{
	public class UpdatePostReplyCommand  : ICommand<PostReply>
	{
		public int Id { get; set; }
		public PostReplyDto PostReplyDto { get; set; }
	}
}
