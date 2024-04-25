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
    public class GetAllCoursesOfProfessorHandler : IQueryHandler<GetAllCoursesOfProfessorQuery, IEnumerable<CourseOfProfessorDto>>
    {
        private readonly IUnitOfwork unitOfwork;

        public GetAllCoursesOfProfessorHandler(IUnitOfwork unitOfwork)
        {
            this.unitOfwork = unitOfwork;
        }

        public async Task<Result<IEnumerable<CourseOfProfessorDto>>> Handle(GetAllCoursesOfProfessorQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var CoursesOfProfessor = await unitOfwork.CourseRepository.GetAllCoursesOfProfessor(request.ProfessorId);

                return Result.Success<IEnumerable<CourseOfProfessorDto>> (CoursesOfProfessor);
            }
            catch (Exception ex) 
            {
                return Result.Failure<IEnumerable<CourseOfProfessorDto>>(new Error(code: "GetAllCoursesOfProfessor", message: ex.Message.ToString()));
            }
        }
    }
}
