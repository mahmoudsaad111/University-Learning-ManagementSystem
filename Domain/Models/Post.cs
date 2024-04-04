namespace Domain.Models
{
	public class Post
	{
		public int PostId { get; set; }
		public string Content { get; set; }
		public DateTime CreatedAt { get; set; }
		public string CreatedBy { get; set; }
		public DateTime UpdatedAt { get; set; }

		// navigation properties
		public bool GlobalToGroup { get; set; }
		public bool IsProfessor { get; set; }
		public int? SectionId { get; set; }
		public Section? Section { get; set; } = null;
		public int? CourseCycleId { get; set; }
		public CourseCycle? CourseCycle { get; set; } = null; 

		public int PublisherId { get; set; }
		public User Publisher { get; set; }
		public ICollection<PostReply> ReplaysOnPost { get; set; }

	}
}
