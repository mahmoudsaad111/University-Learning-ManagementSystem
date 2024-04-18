using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Contract.Dto;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Query.Faculties
{
    public class GetLessInfoAllFacultiesHandler : IQueryHandler<GetLessInfoAllFacultiesQuery, IEnumerable<NameIdDto>>
    {
        private readonly IUnitOfwork unitOfwork;

        public GetLessInfoAllFacultiesHandler(IUnitOfwork unitOfwork)
        {
            this.unitOfwork = unitOfwork;
        }

        public async Task<Result<IEnumerable<NameIdDto>>> Handle(GetLessInfoAllFacultiesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var Faculties =await unitOfwork.FacultyRepository.GetLessInfoFaculties();
                return Result.Success<IEnumerable<NameIdDto>>(Faculties);
            }
            catch (Exception ex)
            {
                return Result.Failure<IEnumerable<NameIdDto>>(new Error(code: "GetLessInfoAllFacultiesQuery", message: ex.Message.ToString()));  
            }
        }
    }
}
