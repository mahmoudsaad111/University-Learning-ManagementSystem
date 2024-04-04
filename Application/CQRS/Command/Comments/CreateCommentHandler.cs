using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Domain.Models;
using Domain.Shared;
 
namespace Application.CQRS.Command.Comments
{
	public class CreateCommentHandler : ICommandHandler<CreateCommentCommand, Comment>
	{
		private readonly IUnitOfwork unitOfwork;

		public CreateCommentHandler(IUnitOfwork unitOfwork)
		{
			this.unitOfwork = unitOfwork;
		}

		public async Task<Result<Comment>> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
		{
			try
			{
				Comment comment =await unitOfwork.CommentRepository.CreateAsync(request.CommentDto.GetComment()) ;
				if (comment is null)
					return Result.Failure<Comment>(new Error(code: "Create Comment" ,message :"not valid data" ));

				await unitOfwork.SaveChangesAsync();
				return Result.Success<Comment>(comment);
			}
			catch(Exception ex) 
			{
				return Result.Failure<Comment>(new Error(code: "Create Comment", message: ex.Message.ToString())) ;
			}
		}
	}
}
