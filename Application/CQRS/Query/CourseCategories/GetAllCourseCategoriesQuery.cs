using Application.Common.Interfaces.CQRSInterfaces;
using Domain.Models;
 
namespace Application.CQRS.Query.CourseCategories
{
    public class GetAllCourseCategoriesQuery :IQuery<IEnumerable<CourseCategory>>
    {

    }
}
