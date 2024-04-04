using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Domain.Models;
using Domain.Shared;
 

namespace Application.CQRS.Command.PostReplies
{
	public class DeletePostReplyHandler : ICommandHandler<DeletePostReplyCommand, int>
	{

		private readonly IUnitOfwork unitOfwork;

		public DeletePostReplyHandler(IUnitOfwork unitOfwork)
		{
			this.unitOfwork = unitOfwork;
		}

		public async Task<Result<int>> Handle(DeletePostReplyCommand request, CancellationToken cancellationToken)
		{
			try
			{
				PostReply postReply = await unitOfwork.PostReplyRepository.GetByIdAsync(request.Id);
				if (postReply == null)
					return Result.Failure<int>(new Error(code: "Delete PostReply", message: "No postReply has this Id")) ;

				if (  
					 (postReply.Content != request.PostReplyDto.Content) ||
					 (postReply.ReplierId != request.PostReplyDto.ReplierId) ||
					 (postReply.PostId!=request.PostReplyDto.PostId)
				   ) 
				{
					return Result.Failure<int>(new Error(code: "Delete PostReply", message: "Data of the postReply is not the same in database"));
				}

				bool IsDeleted = await unitOfwork.PostReplyRepository.DeleteAsync(request.Id);

				if (IsDeleted) 
				{
					int NumOfTasks = await unitOfwork.SaveChangesAsync();
					return Result.Success<int>(request.Id);
				}
				return Result.Failure<int>(new Error(code: "Delete PostReply", message: "Unable To Delete")) ;
			}
			catch(Exception ex) 
			{
				return Result.Failure<int>(new Error(code: "Delete PostReply", message: ex.Message.ToString()));
			}
		}
	}
}
