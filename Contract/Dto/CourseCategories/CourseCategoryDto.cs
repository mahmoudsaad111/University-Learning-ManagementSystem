using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Dto.CourseCategories
{
	public class CourseCategoryDto
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public int DepartementId { get; set; }

		public CourseCategory GetCourseCategory()
		{
			return new CourseCategory
			{
				Name = Name,
				Description = Description,
				DepartementId = DepartementId
			};
		}
	}
}
