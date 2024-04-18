using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Domain.Models;
using Domain.Shared;
 
namespace Application.CQRS.Command.CommentReplies
{
	public class DeleteCommentReplyHandler : ICommandHandler<DeleteCommentReplyCommand, int>
	{

		private readonly IUnitOfwork unitOfwork;

		public DeleteCommentReplyHandler(IUnitOfwork unitOfwork)
		{
			this.unitOfwork = unitOfwork;
		}

		public async Task<Result<int>> Handle(DeleteCommentReplyCommand request, CancellationToken cancellationToken)
		{
			try
			{
				CommentReply commentReply = await unitOfwork.CommentReplyRepository.GetByIdAsync(request.Id);
				if (commentReply == null)
					return Result.Failure<int>(new Error(code: "Delete CommentReply", message: "No CommentReply has this Id")) ;

				//if ((commentReply.CommentId != request.CommentReplyDto.CommentId) ||
				//	 (commentReply.UserId != request.CommentReplyDto.UserId) ||
				//	 (commentReply.Content != request.CommentReplyDto.Content) 					 
				//	) 
				//{
				//	return Result.Failure<int>(new Error(code: "Delete CommentReply", message: "Data of the CommentReply is not the same in database"));
				//}

				bool IsDeleted = await unitOfwork.CommentReplyRepository.DeleteAsync(request.Id);

				if (IsDeleted) 
				{
					int NumOfTasks = await unitOfwork.SaveChangesAsync();
					return Result.Success<int>(request.Id);
				}
				return Result.Failure<int>(new Error(code: "Delete CommentReply", message: "Unable To Delete")) ;
			}
			catch(Exception ex) 
			{
				return Result.Failure<int>(new Error(code: "Delete CommentReply", message: ex.Message.ToString()));
			}
		}
	}
}
