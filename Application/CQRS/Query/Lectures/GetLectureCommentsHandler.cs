using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Domain.Models;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Query.Lectures
{
    public class GetLectureCommentsHandler : IQueryHandler<GetLectureCommentsQuery, IEnumerable<Comment>>
    {
        private readonly IUnitOfwork unitOfwork;

        public GetLectureCommentsHandler(IUnitOfwork unitOfwork)
        {
            this.unitOfwork = unitOfwork;
        }
        public async Task<Result<IEnumerable<Comment>>> Handle(GetLectureCommentsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var comments = await unitOfwork.LectureRepository.GetLectureComments(request.LectureId);
                return Result.Create<IEnumerable<Comment>>(comments);
            }
            catch (Exception ex)
            {
                return Result.Failure<IEnumerable<Comment>>(new Error(code: "GetLectureComments", message: ex.Message.ToString()));
            }
        }
    }
}
