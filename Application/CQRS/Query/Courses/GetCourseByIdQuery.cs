using Application.Common.Interfaces.CQRSInterfaces;
using Domain.Models;

namespace Application.CQRS.Query.Courses
{
    public class GetCourseByIdQuery : IQuery<Course>
    {
        public int CourseId { get; set; }   
    }
}
