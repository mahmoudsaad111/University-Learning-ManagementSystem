using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Contract.Dto.Courses;
using Domain.Models;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Query.Courses
{
    public class GetAllCoursesOfAcadimicYearHandler : IQueryHandler<GetAllCoursesOfAcadimicYearQuery, IEnumerable<CourseLessInfoDto>>
    {
        private readonly IUnitOfwork unitOfwork;

        public GetAllCoursesOfAcadimicYearHandler(IUnitOfwork unitOfwork)
        {
            this.unitOfwork = unitOfwork;
        }
        public async Task<Result<IEnumerable<CourseLessInfoDto>>> Handle(GetAllCoursesOfAcadimicYearQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var Courses = await unitOfwork.CourseRepository.GetAllCoursesOfAcadimicYearAndCourseCategory(request.AcadimicYearId, request.CourseCategoryId);
                return Result.Create<IEnumerable<CourseLessInfoDto>>(Courses);
            }
            catch (Exception ex)
            {
                return Result.Failure<IEnumerable<CourseLessInfoDto>>(new Error(code: "GetAllCourses", message: ex.Message.ToString()));
            }
        }
    }
}
