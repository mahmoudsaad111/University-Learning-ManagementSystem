using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Contract.Dto.Posts;
using Domain.Models;
using Domain.Shared;
 
namespace Application.CQRS.Command.Posts
{
	public class DeletePostHandler : ICommandHandler<DeletePostCommand, int>
	{

		private readonly IUnitOfwork unitOfwork;

		public DeletePostHandler(IUnitOfwork unitOfwork)
		{
			this.unitOfwork = unitOfwork;
		}

		public async Task<Result<int>> Handle(DeletePostCommand request, CancellationToken cancellationToken)
		{
			try
			{
				Post post = await unitOfwork.PostRepository.GetByIdAsync(request.Id);
				if (post == null)
					return Result.Failure<int>(new Error(code: "Delete Post", message: "No post has this Id")) ;

				//if ((post.PublisherId != request.PostDto.PublisherId) ||
				//	 (post.IsProfessor != request.PostDto.IsProfessor) ||
				//	 (post.CourseCycleId != request.PostDto.CourseCycleId) ||
				//	 (post.Content!=request.PostDto.Content) ||
				//	 (post.SectionId!=request.PostDto.SectionId) ||
				//	 (post.CreatedBy!=request.PostDto.CreatedBy)
				//	) 
				//{
				//	return Result.Failure<int>(new Error(code: "Delete Post", message: "Data of the post is not the same in database"));
				//}

				bool IsDeleted = await unitOfwork.PostRepository.DeleteAsync(request.Id);

				if (IsDeleted) 
				{
					int NumOfTasks = await unitOfwork.SaveChangesAsync();
					return Result.Success<int>(request.Id);
				}
				return Result.Failure<int>(new Error(code: "Delete Post", message: "Unable To Delete")) ;
			}
			catch(Exception ex) 
			{
				return Result.Failure<int>(new Error(code: "Delete Post", message: ex.Message.ToString()));
			}
		}
	}
}
