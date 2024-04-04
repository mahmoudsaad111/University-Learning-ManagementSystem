
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
	public class Professor  
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int ProfessorId { get; set;  }
		public string? Specification { get; set; } = null!;
		//-- navigation properties
		public User User { get; set; }
		public int? DepartementId { get; set; }
		public Departement Departement { get; set; }
		public ICollection<CourseCycle> CoursesCycles { get; set;}

	}
}