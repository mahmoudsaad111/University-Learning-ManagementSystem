using Application.Common.Interfaces.CQRSInterfaces;
using Contract.Dto.Comments;
 
using Domain.Models;
 
namespace Application.CQRS.Command.Comments
{
	public class UpdateCommentCommand  : ICommand<Comment>
	{
		public int Id { get; set; }
		public CommentDto CommentDto { get; set; }
	}
}
