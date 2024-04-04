using Application.Common.Interfaces.CQRSInterfaces;
using Contract.Dto.CourseCategories;
using Domain.Models;
 

namespace Application.CQRS.Command.CourseCategories
{
    public class UpdateCourseCategoryCommand :ICommand<CourseCategory>
    {
        public int Id { get; set; } 
        public CourseCategoryDto CourseCategoryDto { get; set; }   
    }
}
