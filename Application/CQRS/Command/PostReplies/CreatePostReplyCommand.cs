using Application.Common.Interfaces.CQRSInterfaces;
using Contract.Dto.PostReplies;
 
using Domain.Models;


namespace Application.CQRS.Command.PostReplies
{
	public class CreatePostReplyCommand : ICommand<PostReply>
	{
		public PostReplyDto PostReplyDto { get; set; }
	}
}
