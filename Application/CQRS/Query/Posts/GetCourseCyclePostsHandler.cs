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
    public class GetCourseCyclePostsHandler : IQueryHandler<GetCourseCyclePostsQuery, IEnumerable<Post>>
    {
        private readonly IUnitOfwork unitOfwork;

        public GetCourseCyclePostsHandler(IUnitOfwork unitOfwork)
        {
            this.unitOfwork = unitOfwork;
        }
        public async Task<Result<IEnumerable<Post>>> Handle(GetCourseCyclePostsQuery request, CancellationToken cancellationToken)
        {
            try
            {
        
                var Posts = await unitOfwork.PostRepository.GetCourseCyclePosts(request.CourseCycleId);
                return Result.Create<IEnumerable<Post>>(Posts);
            }
            catch (Exception ex)
            {
                return Result.Failure<IEnumerable<Post>>(new Error(code: "GetcourseCyclePosts", message: ex.Message.ToString()));
            }
        }
    }
}
