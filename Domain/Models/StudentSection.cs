namespace Domain.Models
{
	public class StudentSection
	{
		public int StudentSectionId { get; set; }	
		public int StudentTotalMarks { get; set; }
		//navigation properties
		public int StudentId { get; set; }
		public Student Student { get; set; }
		public int SectionId { get; set; }
		public Section Section { get; set; }	

	}
}
