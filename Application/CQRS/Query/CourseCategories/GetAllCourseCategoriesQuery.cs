using Application.Common.Interfaces.CQRSInterfaces;
using Contract.Dto.CourseCategories;
using Domain.Models;
 
namespace Application.CQRS.Query.CourseCategories
{
    public class GetAllCourseCategoriesQuery :IQuery<IEnumerable<CourseCategoryWithDeptAndFacultyDto>>
    {

    }
}
