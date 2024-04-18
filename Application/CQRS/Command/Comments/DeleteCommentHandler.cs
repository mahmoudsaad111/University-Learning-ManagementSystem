using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Contract.Dto.Comments;
using Domain.Models;
using Domain.Shared;
 
namespace Application.CQRS.Command.Comments
{
	public class DeleteCommentHandler : ICommandHandler<DeleteCommentCommand, int>
	{

		private readonly IUnitOfwork unitOfwork;

		public DeleteCommentHandler(IUnitOfwork unitOfwork)
		{
			this.unitOfwork = unitOfwork;
		}

		public async Task<Result<int>> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
		{
			try
			{
				Comment comment = await unitOfwork.CommentRepository.GetByIdAsync(request.Id);
				if (comment == null)
					return  Result.Failure<int>(new Error(code: "Delete Comment", message: "No Comment has this Id")) ;

				//if ((comment.LectureId != request.CommentDto.LectureId) ||
				//	 (comment.UserId != request.CommentDto.UserId) ||					 
				//	 (comment.Content!=request.CommentDto.Content)
				//	) 
				//{
				//	return Result.Failure<int>(new Error(code: "Delete Comment", message: "Data of the Comment is not the same in database"));
				//}

				bool IsDeleted = await unitOfwork.CommentRepository.DeleteAsync(request.Id);

				if (IsDeleted) 
				{
					int NumOfTasks = await unitOfwork.SaveChangesAsync();
					return Result.Success<int>(request.Id);
				}
				return Result.Failure<int>(new Error(code: "Delete Comment", message: "Unable To Delete")) ;
			}
			catch(Exception ex) 
			{
				return Result.Failure<int>(new Error(code: "Delete Comment", message: ex.Message.ToString()));
			}
		}
	}
}
