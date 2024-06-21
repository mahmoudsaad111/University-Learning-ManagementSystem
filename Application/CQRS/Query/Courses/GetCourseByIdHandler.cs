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
    public class GetCourseById : IQueryHandler<GetCourseByIdQuery,Course>
    {
        private readonly IUnitOfwork unitOfwork;

        public GetCourseById(IUnitOfwork unitOfwork)
        {
            this.unitOfwork = unitOfwork;
        }
        public async Task<Result<Course>> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var CoursesOfStudent = await unitOfwork.CourseRepository.GetByIdAsync(request.CourseId);
                return Result.Success<Course>(CoursesOfStudent);
            }
            catch (Exception ex)
            {
                return Result.Failure<Course>(new Error(code: "GetAllCoursesOfStudent", message: ex.Message.ToString()));
            }
        }
    }
}
