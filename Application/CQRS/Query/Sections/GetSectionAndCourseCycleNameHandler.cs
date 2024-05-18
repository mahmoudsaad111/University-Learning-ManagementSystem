using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Contract.Dto.Sections;
using Domain.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Query.Sections
{
    public class GetSectionAndCourseCycleNameHandler : IQueryHandler<GetSectionAndCourseCycleNameQuery,SectionAndCourseCycleNameDto>
    {
        private readonly IUnitOfwork unitOfwork;

        public GetSectionAndCourseCycleNameHandler(IUnitOfwork unitOfwork)
        {
            this.unitOfwork = unitOfwork;
        }
        public async Task<Result<SectionAndCourseCycleNameDto>> Handle(GetSectionAndCourseCycleNameQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var SectionAndCourseCycleName = await unitOfwork.SectionRepository.GetSectionAndCourseCycleName(request.SectionId);
                if (SectionAndCourseCycleName is null)
                    return Result.Failure<SectionAndCourseCycleNameDto>(new Error(code: "GetSectionAndCourseCycleNameHandler", message: "In-valid data"));

                return Result.Success(SectionAndCourseCycleName);
            }
            catch (Exception ex)
            {
                return Result.Failure<SectionAndCourseCycleNameDto>(new Error(code: "GetSectionAndCourseCycleNameHandler", message: ex.Message.ToString()));
            }
        }
    }
}
