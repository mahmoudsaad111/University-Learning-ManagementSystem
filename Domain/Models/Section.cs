namespace Domain.Models
{
	public class Section
	{
		public int SectionId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }

		// navigation properties
		public ICollection<Post> Posts { get; set; }	
		public ICollection<StudentSection> StudentsInSection { get; set; }
		public ICollection<Lecture> Lectures{ get; set; }
		public ICollection<Assignment> Assignments { get; set; }
		public int InstructorId { get;set; }
		public Instructor Instructor { get; set; }
		public int CourseCycleId { get; set; }
		public CourseCycle CourseCycle { get; set; }
	}
}
