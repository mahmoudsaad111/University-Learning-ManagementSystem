
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
	public class Group
	{
		public int GroupId { get; set; }
		public string Name { get; set; } 
		public string StudentHeadName { get; set; } = null!;
		public string StudentHeadPhone { get; set; } = null!;
		public short NumberOfStudent { get; set; }

		//-navigation properties
	    public int AcadimicYearId { get; set; }	
		public AcadimicYear AcadimicYear { get; set; }
		public ICollection<Student > Students { get; set; }
		public ICollection<CourseCycle> CourseCycles { get; set; }

	}
}