using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Domain.Models;
using Domain.Shared;
 

namespace Application.CQRS.Command.CommentReplies
{
	public class UpdateCommentReplyHandler : ICommandHandler<UpdateCommentReplyCommand, CommentReply>
	{
		private readonly IUnitOfwork unitOfwork; 
		public UpdateCommentReplyHandler (IUnitOfwork unitOfwork)
		{
			this.unitOfwork = unitOfwork;
		}
		public async Task<Result<CommentReply>> Handle(UpdateCommentReplyCommand request, CancellationToken cancellationToken)
		{
			try
			{
				CommentReply commentReply = unitOfwork.CommentReplyRepository.Find(f => f.CommentReplyId == request.Id);
				if (commentReply is null)
					return Result.Failure<CommentReply>(new Error(code: "Update CommentReply", message: "No commentReply exist by this Id"));

				commentReply.Content=request.CommentReplyDto.Content;

				int NumOfTasks  = await unitOfwork.SaveChangesAsync();
				return Result.Success<CommentReply>(commentReply);
			}
			catch (Exception ex)
			{
				return Result.Failure<CommentReply>(new Error(code: "Updated CommentReply" , message: ex.Message.ToString())); 
			}
		}
	}
}
