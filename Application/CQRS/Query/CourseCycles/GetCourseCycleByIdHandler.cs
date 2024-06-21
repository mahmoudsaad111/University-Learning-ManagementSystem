using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Domain.Models;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Query.CourseCycles
{
    public class GetCourseCycleById : IQueryHandler<GetCourseCycleByIdQuery, CourseCycle>
    {
        private readonly IUnitOfwork unitOfwork;
        public GetCourseCycleById(IUnitOfwork unitOfwork)
        {
            this.unitOfwork = unitOfwork;
        }

        public async Task<Result<CourseCycle>> Handle(GetCourseCycleByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var CourseCycle = await unitOfwork.CourseCycleRepository.GetByIdAsync(request.CourseCycleId);
                return Result.Create<CourseCycle>(CourseCycle);
            }
            catch (Exception ex)
            {
                return Result.Failure<CourseCycle>(new Error(code: "GetAllCourseCycles", message: ex.Message.ToString()));
            }
        }
    }
}
