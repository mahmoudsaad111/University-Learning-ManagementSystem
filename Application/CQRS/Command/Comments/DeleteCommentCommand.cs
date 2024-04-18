using Application.Common.Interfaces.CQRSInterfaces; 
using Contract.Dto.Comments;
 
 
namespace Application.CQRS.Command.Comments
{
	public class DeleteCommentCommand : ICommand<int>
	{
		public int Id { get; set; }	
		//public CommentDto CommentDto { get; set; }
	}
}
