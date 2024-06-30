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
    public class GetSectionPostsHandler : IQueryHandler<GetSectionPostsQuery, IEnumerable<Post>>
    {
        private readonly IUnitOfwork unitOfwork;

        public GetSectionPostsHandler(IUnitOfwork unitOfwork)
        {
            this.unitOfwork = unitOfwork;
        }
        public async Task<Result<IEnumerable<Post>>> Handle(GetSectionPostsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var Posts = await unitOfwork.PostRepository.GetSectionPosts(request.SectionId);
                return Result.Create<IEnumerable<Post>>(Posts);
            }
            catch (Exception ex)
            {
                return Result.Failure<IEnumerable<Post>>(new Error(code: "GetSectionPosts", message: ex.Message.ToString()));
            }
        }
    }
}
