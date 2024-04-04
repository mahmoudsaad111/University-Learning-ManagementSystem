using Application.Common.Interfaces.CQRSInterfaces;
using Contract.Dto.CommentReplies;
 
using Domain.Models;
 
namespace Application.CQRS.Command.CommentReplies
{
	public class UpdateCommentReplyCommand  : ICommand<CommentReply>
	{
		public int Id { get; set; }
		public CommentReplyDto CommentReplyDto { get; set; }
	}
}
