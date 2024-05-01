namespace Domain.Models
{
	public class CourseCycle // Intermediate table between Group and Course
	{
		public int CourseCycleId { get; set; }
		public string Title { get; set; }
		// navigation properties
		public ICollection<Post> Posts { get; set; }
		public ICollection <Lecture> Lectures { get;set; }
		public ICollection <Section> Sections { get; set; }	
		public ICollection<StudentCourseCycle> StudentsInCourseCycle { get; set; }
		public int GroupId { get; set; }	
		public Group Group { get; set; }
		public int CourseId { get; set; }
		public Course Course { get; set; }
		public int ProfessorId { get; set; }
		public Professor Professor { get; set; } = null!;
		
	}
}
