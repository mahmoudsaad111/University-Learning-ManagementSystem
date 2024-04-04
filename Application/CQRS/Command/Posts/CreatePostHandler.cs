using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Application.CQRS.Command.Posts;
using Domain.Models;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Command.Posts
{
    public class CreatePostHandler : ICommandHandler<CreatePostCommand, Post>
	{
        private readonly IUnitOfwork unitOfwork;

        public CreatePostHandler    (IUnitOfwork unitOfwork)   
        {
            this.unitOfwork = unitOfwork;
        }

 

        public async Task<Result<Post>> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            try
            {

                if (
                    (request.PostDto.SectionId == 0 && request.PostDto.CourseCycleId == 0) ||
                    (request.PostDto.SectionId != 0 && request.PostDto.CourseCycleId != 0)
                  )
                    return Result.Failure<Post>(new Error(code: "Create Post", message: "Post should be belong to one Course cycle and created by Prof or belong to section and created by instructor"));
                if (request.PostDto.GlobalToGroup == true && request.PostDto.SectionId != 0)
                    return Result.Failure<Post>(new Error(code: "Create Post", message: "when the creator's type is instructor the GlobalToGroup must be zero, only the Prof can make Posts global to Group"));


                Post post = await unitOfwork.PostRepository.CreateAsync(request.PostDto.GetPost());
                if (post is null)
                    return Result.Failure<Post>(new Error(code: "Create Post", message: "not valid data"));


                await unitOfwork.SaveChangesAsync();
                return Result.Success<Post>(post);
            }
            catch (Exception ex)
            {
                return Result.Failure<Post>(new Error(code: "Create Post", message: ex.Message.ToString()));
            }
        }
    }
}
