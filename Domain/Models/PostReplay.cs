namespace Domain.Models
{
	public class PostReply
	{
		public int PostReplyId { get; set; }
		public string Content { get; set; }
		public string CreatedBy { get; set; }
		public DateTime CreatedAt { get; set;}
		public DateTime UpdatedAt { get; set;}
	    
		// navigation properties
		public int PostId { get; set; }	
		public Post Post { get; set; }
		public int ReplierId { get; set; }
		public User Replier { get; set; }
	}
}
