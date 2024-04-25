using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Contract.Dto.Sections;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Query.Sections
{
    public class GetSectionOfCourseToStudentHandler : IQueryHandler<GetSectionOfCourseToStudentQuery, SectionsOfCoursesToStudentDto>
    {
        private readonly IUnitOfwork unitOfwork;

        public GetSectionOfCourseToStudentHandler(IUnitOfwork unitOfwork)
        {
            this.unitOfwork = unitOfwork;
        }
        public async Task<Result<SectionsOfCoursesToStudentDto>> Handle(GetSectionOfCourseToStudentQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var SectionOfCourseToUser = await unitOfwork.SectionRepository.GetSectionOfCoursesToStudent(StudentId: request.StudentId, CourseCycleId: request.CourseCycleId);
                return Result.Success<SectionsOfCoursesToStudentDto>(SectionOfCourseToUser);
            }
            catch (Exception ex)
            {
                return Result.Failure<SectionsOfCoursesToStudentDto>(new Error(code: "GetSectionOfCourseToStudent", message: ex.Message.ToString()));
            }
        }
    }
}
