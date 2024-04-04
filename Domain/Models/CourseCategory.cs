namespace Domain.Models
{
	public class CourseCategory
	{
		public int CourseCategoryId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		// navigation prop
		public ICollection<Course> Courses { get; set; }
		public int DepartementId { get; set; }
		public Departement Departement { get; set; }

	}
}
