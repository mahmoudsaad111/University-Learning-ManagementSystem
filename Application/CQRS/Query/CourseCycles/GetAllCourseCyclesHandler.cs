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
    public class GetAllCourseCyclesHandler : IQueryHandler<GetAllCourseCyclesQuery, IEnumerable<CourseCycle>>
    {
        private readonly IUnitOfwork unitOfwork;
        public GetAllCourseCyclesHandler(IUnitOfwork unitOfwork)
        {
            this.unitOfwork = unitOfwork;
        }

        public async Task<Result<IEnumerable<CourseCycle>>> Handle(GetAllCourseCyclesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var CourseCycles = await unitOfwork.CourseCycleRepository.FindAllAsyncInclude();
                return Result.Create<IEnumerable<CourseCycle>>(CourseCycles);
            }
            catch (Exception ex)
            {
                return Result.Failure<IEnumerable<CourseCycle>>(new Error(code: "GetAllCourseCycles", message: ex.Message.ToString()));
            }
        }
    }
}
