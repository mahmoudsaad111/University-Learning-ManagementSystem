
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
	public class Instructor 
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int InstructorId { get; set; }
		public string? Specification { get ; set; }= null!;	
	
		//-- navigation properties	 
		public User User { get; set; }
		public int? DepartementId { get; set; }

		public virtual Departement Departement { get; set; }

		public virtual ICollection<Section> Sections { get; set; }
	}
}