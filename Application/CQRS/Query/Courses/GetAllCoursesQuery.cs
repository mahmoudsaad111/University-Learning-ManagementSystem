using Application.Common.Interfaces.CQRSInterfaces;
using Domain.Models;

namespace Application.CQRS.Query.Courses
{
    public class GetAllCoursesQuery :IQuery<IEnumerable<Course>>
    {
    }
}
