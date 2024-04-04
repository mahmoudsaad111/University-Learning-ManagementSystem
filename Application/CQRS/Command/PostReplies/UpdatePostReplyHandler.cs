using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Domain.Models;
using Domain.Shared;
 

namespace Application.CQRS.Command.PostReplies
{
	public class UpdatePostReplyHandler : ICommandHandler<UpdatePostReplyCommand, PostReply>
	{
		private readonly IUnitOfwork unitOfwork; 
		public UpdatePostReplyHandler (IUnitOfwork unitOfwork)
		{
			this.unitOfwork = unitOfwork;
		}
		public async Task<Result<PostReply>> Handle(UpdatePostReplyCommand request, CancellationToken cancellationToken)
		{
			try
			{
				PostReply postReply = unitOfwork.PostReplyRepository.Find(f => f.PostReplyId == request.Id);
				if (postReply is null)
					return Result.Failure<PostReply>(new Error(code: "Update PostReply", message: "No PostReply exist by this Id"));
				
			 
				postReply.Content = request.PostReplyDto.Content;
				 
				int NumOfTasks  = await unitOfwork.SaveChangesAsync();
				return Result.Success<PostReply>(postReply);
			}
			catch (Exception ex)
			{
				return Result.Failure<PostReply>(new Error(code: "Updated PostReply" , message: ex.Message.ToString())); 
			}
		}
	}
}
