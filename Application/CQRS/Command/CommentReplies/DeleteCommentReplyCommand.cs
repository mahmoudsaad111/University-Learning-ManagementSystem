using Application.Common.Interfaces.CQRSInterfaces; 
using Contract.Dto.CommentReplies;
 
 
namespace Application.CQRS.Command.CommentReplies
{
	public class DeleteCommentReplyCommand : ICommand<int>
	{
		public int Id { get; set; }	
		public CommentReplyDto CommentReplyDto { get; set; }
	}
}
