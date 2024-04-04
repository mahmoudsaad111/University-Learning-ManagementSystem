using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Domain.Models;
using Domain.Shared;
 

namespace Application.CQRS.Command.Posts
{
	public class UpdatePostHandler : ICommandHandler<UpdatePostCommand, Post>
	{
		private readonly IUnitOfwork unitOfwork; 
		public UpdatePostHandler (IUnitOfwork unitOfwork)
		{
			this.unitOfwork = unitOfwork;
		}
		public async Task<Result<Post>> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
		{
			try
			{
				Post post = unitOfwork.PostRepository.Find(f => f.PostId == request.Id);
				if (post is null)
					return Result.Failure<Post>(new Error(code: "Update Post", message: "No Post exist by this Id"));



                if (
					 (request.PostDto.SectionId == 0 && request.PostDto.CourseCycleId == 0) ||
					 (request.PostDto.SectionId != 0 && request.PostDto.CourseCycleId != 0)
                   )
                    return Result.Failure<Post>(new Error(code: "Updated Post", message: "Post should be belong to one Course cycle and created by Prof or belong to section and created by instructor"));
               
				if (request.PostDto.GlobalToGroup == true && request.PostDto.SectionId != 0)
                    return Result.Failure<Post>(new Error(code: "Updated Post", message: "when the creator's type is instructor the GlobalToGroup must be zero, only the Prof can make Posts global to Group"));

				var GetPostFromPostDto = request.PostDto.GetPost();

				post.CreatedBy = GetPostFromPostDto.CreatedBy;
				post.Content = GetPostFromPostDto.Content;
                post.PublisherId = GetPostFromPostDto.PublisherId;
				post.GlobalToGroup= GetPostFromPostDto.GlobalToGroup;
				post.CourseCycleId= GetPostFromPostDto.CourseCycleId;
				post.SectionId= GetPostFromPostDto.SectionId;
				post.IsProfessor = GetPostFromPostDto.IsProfessor;

				int NumOfTasks  = await unitOfwork.SaveChangesAsync();
				return Result.Success<Post>(post);
			}
			catch (Exception ex)
			{
				return Result.Failure<Post>(new Error(code: "Updated Post" , message: ex.Message.ToString())); 
			}
		}
	}
}
