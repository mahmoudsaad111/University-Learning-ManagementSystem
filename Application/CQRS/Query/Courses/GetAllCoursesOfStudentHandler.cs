using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Contract.Dto.Courses;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Query.Courses
{
    public class GetAllCoursesOfStudentHandler : IQueryHandler<GetAllCoursesOfStudentQuery, IEnumerable<CourseOfStudentDto>>
    {
        private readonly IUnitOfwork unitOfwork;

        public GetAllCoursesOfStudentHandler(IUnitOfwork unitOfwork)
        {
            this.unitOfwork = unitOfwork;
        }
        public async Task<Result<IEnumerable<CourseOfStudentDto>>> Handle(GetAllCoursesOfStudentQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var CoursesOfStudent = await unitOfwork.CourseRepository.GetAllCoursesOfStudent(StudentId :request.StudentId );
                return Result.Success<IEnumerable<CourseOfStudentDto>>(CoursesOfStudent);
            }
            catch (Exception ex)
            {
                return Result.Failure<IEnumerable<CourseOfStudentDto>>(new Error(code: "GetAllCoursesOfStudent", message: ex.Message.ToString()));
            }
        }
    }
}
