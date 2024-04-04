using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
 
using Domain.Models;
using Domain.Shared;
 

namespace Application.CQRS.Command.PostReplies
{
    public class CreatePostReplyHandler : ICommandHandler<CreatePostReplyCommand, PostReply>
	{
        private readonly IUnitOfwork unitOfwork;

        public CreatePostReplyHandler    (IUnitOfwork unitOfwork)   
        {
            this.unitOfwork = unitOfwork;
        }

 

        public async Task<Result<PostReply>> Handle(CreatePostReplyCommand request, CancellationToken cancellationToken)
        {
            try
            {
                PostReply postReply = await unitOfwork.PostReplyRepository.CreateAsync(request.PostReplyDto.GetPostReply());
                if (postReply is null)
                    return Result.Failure<PostReply>(new Error(code: "Create PostReply", message: "not valid data"));

                await unitOfwork.SaveChangesAsync();
                return Result.Success<PostReply>(postReply);
            }
            catch (Exception ex)
            {
                return Result.Failure<PostReply>(new Error(code: "Create PostReply", message: ex.Message.ToString()));
            }
        }
    }
}
