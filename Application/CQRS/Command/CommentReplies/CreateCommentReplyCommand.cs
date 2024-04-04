using Application.Common.Interfaces.CQRSInterfaces;
using Contract.Dto.CommentReplies;
using Domain.Models;
 
namespace Application.CQRS.Command.CommentReplies
{
	public class CreateCommentReplyCommand :ICommand <CommentReply>
	{
		public CommentReplyDto commentReplyDto { get; set; }
	}
}
