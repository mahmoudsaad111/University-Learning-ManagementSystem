using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Domain.Models;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Query.Posts
{
    public class GetAllPostsHandler : IQueryHandler<GetAllPostsQuery, IEnumerable<Post>>
    {
        private readonly IUnitOfwork unitOfwork;

        public GetAllPostsHandler(IUnitOfwork unitOfwork)
        {
            this.unitOfwork = unitOfwork;
        }
        public async Task<Result<IEnumerable<Post>>> Handle(GetAllPostsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var Posts = await unitOfwork.PostRepository.FindAllAsyncInclude();
                return Result.Create<IEnumerable<Post>>(Posts);
            }
            catch (Exception ex)
            {
                return Result.Failure<IEnumerable<Post>>(new Error(code: "GetAllPosts", message: ex.Message.ToString()));
            }
        }
    }
}
