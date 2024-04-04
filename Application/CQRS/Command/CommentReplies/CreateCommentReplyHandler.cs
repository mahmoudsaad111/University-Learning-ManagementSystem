using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Domain.Models;
using Domain.Shared;
 
namespace Application.CQRS.Command.CommentReplies
{
	public class CreateCommentReplyHandler : ICommandHandler<CreateCommentReplyCommand, CommentReply>
	{
		private readonly IUnitOfwork unitOfwork;

		public CreateCommentReplyHandler(IUnitOfwork unitOfwork)
		{
			this.unitOfwork = unitOfwork;
		}

		public async Task<Result<CommentReply>> Handle(CreateCommentReplyCommand request, CancellationToken cancellationToken)
		{
			try
			{
				CommentReply commentReply =await unitOfwork.CommentReplyRepository.CreateAsync(request.commentReplyDto.GetCommentReply()) ;
				if (commentReply is null)
					return Result.Failure<CommentReply>(new Error(code: "Create CommentReply" ,message :"not valid data" ));

				await unitOfwork.SaveChangesAsync();
				return Result.Success<CommentReply>(commentReply);
			}
			catch(Exception ex) 
			{
				return Result.Failure<CommentReply>(new Error(code: "Create CommentReply", message: ex.Message.ToString())) ;
			}
		}
	}
}
