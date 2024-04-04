using Application.Common.Interfaces.CQRSInterfaces;
using Contract.Dto.CourseCategories;
using Contract.Dto.Faculties;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Command.CourseCategories
{
    public class DeleteCourseCategoryCommand :ICommand<int>
    {
        public int Id { get; set; }
        public CourseCategoryDto CourseCategoryDto { get; set; }
    }
}
