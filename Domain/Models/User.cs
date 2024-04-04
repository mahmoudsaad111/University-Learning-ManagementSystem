
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
 
	public class User :IdentityUser<int>  
	{
		
		/*
		 * all comments have perfix  $% is commented because already exist on IdentityUser
		 */

		//$%	public int UserId { get; set; }
		public string FirstName { get; set; } = null!;
		public string SecondName { get; set; } = null!;
		public string ThirdName { get; set; } = null!;
		public string FourthName { get; set; } = null!;
		public string FullName { get {return FirstName +" "+SecondName+" "+ThirdName+" "+FourthName; } }
		//$%		public string Email { get; set; } = null!;
		//$%		public string PhoneNumber { get; set; } = null!;
		public string Address { get; set; } = null!;
		public bool Gender { get; set; }
		public DateTime BirthDay { get; set; }	=	DateTime.MinValue;
		public DateTime? CreatedAt { get; set; }= DateTime.Now;
		public DateTime? UpdatedAt { get; set;}= DateTime.Now;
		public string ImageUrl { get; set; }	

		//-- navigation properties
		public Student Student { get; set; }
		public Professor Professor { get; set; }
		public Instructor Instructor { get; set; }

		[InverseProperty("UserFrom")]		
		public ICollection<Notification>?Froms { get; set; }
		[InverseProperty("UserTo")]
		public ICollection<Notification>?Tos { get; set; }
		public ICollection<Post> Posts { get; set; }
		public ICollection<PostReply> ReplaysOnPosts { get; set; }

		public ICollection<Comment>Comments { get; set; }

        public ICollection<CommentReply> CommentReplies { get; set; }

	}
}