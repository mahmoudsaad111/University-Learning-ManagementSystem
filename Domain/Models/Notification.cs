
//using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
	public class Notification
	{
		public int NotificationId { get; set; }
		public int FromUserId { get; set; }
		public int ToUserId { get; set; }
		public string Content { get;set; } = null!;
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }

		//-- navigation properties
		public int UserIdFrom { get; set; }
		public int UserIdTo { get; set;}

		[ForeignKey("UserIdForm")]
		public User UserFrom { get; set; }
		
		[ForeignKey("UserIdTo")]
		public User UserTo { get; set; }
	}
}