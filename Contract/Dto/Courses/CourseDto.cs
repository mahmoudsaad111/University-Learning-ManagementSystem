using Domain.Models;


namespace Contract.Dto.Courses
{
	public class CourseDto
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public int TotalMark { get; set; }
		public int AcadimicYearId { get; set; }
		public int CourseCategoryId { get; set; }
		public int DepartementId { get; set; }     

		public Course GetCourse()
		{
			return new Course
			{
				Name = Name,
				Description = Description,
				TotalMark = TotalMark,
				CourseCategoryId = CourseCategoryId==0?1:CourseCategoryId,
				AcadimicYearId=AcadimicYearId
			};				
		}
	}
}
