using Application.Common.Interfaces.CQRSInterfaces;
using Contract.Dto.Comments;
using Domain.Models;
 
namespace Application.CQRS.Command.Comments
{
	public class CreateCommentCommand :ICommand <Comment>
	{
		public CommentDto CommentDto { get; set; }
	}
}
