using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Domain.Models;
using Domain.Shared;
 
namespace Application.CQRS.Command.Comments
{
	public class UpdateCommentHandler : ICommandHandler<UpdateCommentCommand, Comment>
	{
		private readonly IUnitOfwork unitOfwork; 
		public UpdateCommentHandler (IUnitOfwork unitOfwork)
		{
			this.unitOfwork = unitOfwork;
		}
		public async Task<Result<Comment>> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
		{
			try
			{
				Comment comment = unitOfwork.CommentRepository.Find(f => f.CommentId == request.Id);
				if (comment is null)
					return Result.Failure<Comment>(new Error(code: "Update comment", message: "No comment exist by this Id"));

				comment.Content = request.CommentDto.Content;

				int NumOfTasks  = await unitOfwork.SaveChangesAsync();
				return Result.Success<Comment>(comment);
			}
			catch (Exception ex)
			{
				return Result.Failure<Comment>(new Error(code: "Updated comment" , message: ex.Message.ToString())); 
			}
		}
	}
}
