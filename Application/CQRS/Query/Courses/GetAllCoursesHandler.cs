using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Application.CQRS.Query.Departements;
using Domain.Models;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Query.Courses
{
    public class GetAllCoursesHandler : IQueryHandler<GetAllCoursesQuery,IEnumerable<Course>>
    {
        private readonly IUnitOfwork unitOfwork;

        public GetAllCoursesHandler(IUnitOfwork unitOfwork)
        {
            this.unitOfwork = unitOfwork;
        }

        public async Task<Result<IEnumerable<Course>>> Handle(GetAllCoursesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var Faculties = await unitOfwork.CourseRepository.FindAllAsyncInclude();
                return Result.Create<IEnumerable<Course>>(Faculties);
            }
            catch (Exception ex)
            {
                return Result.Failure<IEnumerable<Course>>(new Error(code: "GetAllCourses", message: ex.Message.ToString()));
            }
        }
    }
}
