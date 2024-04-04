using Application.Common.Interfaces.CQRSInterfaces;
using Contract.Dto.CourseCategories;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Command.CourseCategories
{
	public class CreateCourseCategoryCommand :ICommand<CourseCategory>
	{
		public CourseCategoryDto CourseCategoryDto { get; set; }
	}
}
