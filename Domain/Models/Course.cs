
namespace Domain.Models
{
	public class Course
	{
		public int CourseId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public int TotalMark { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }

		// navigation properties
		public int CourseCategoryId { get; set; }
		public CourseCategory CourseCategory { get; set; }
        public int AcadimicYearId { get; set; }	
		public AcadimicYear AcadimicYear { get; set; }	
		public ICollection<CourseCycle> CourseCycles { get; set; }
	}
}
