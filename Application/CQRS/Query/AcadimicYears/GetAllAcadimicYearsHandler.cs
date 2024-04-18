using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Application.CQRS.Query.CourseCategories;
using Domain.Models;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Query.AcadimicYears
{
    public class GetAllAcadimicYearsHandler : IQueryHandler<GetAllAcadimicYearsQuery, IEnumerable<AcadimicYear>>
    {
        private readonly IUnitOfwork unitOfwork;

        public GetAllAcadimicYearsHandler(IUnitOfwork unitOfwork)
        {
            this.unitOfwork = unitOfwork;
        }
        public async Task<Result<IEnumerable<AcadimicYear>>> Handle(GetAllAcadimicYearsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var acadimicYears = await unitOfwork.AcadimicYearRepository.GetAllAcadimicYearsOfDepartement(request.DepartementId) ;
                return Result.Create<IEnumerable<AcadimicYear>>(acadimicYears);
            }
            catch (Exception ex)
            {
                return Result.Failure<IEnumerable<AcadimicYear>>(new Error(code: "GetAllAcadimicYears", message: ex.Message.ToString()));
            }
        }
    }
}
