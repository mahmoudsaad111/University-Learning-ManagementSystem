
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Models
{
	public class Departement
	{
		public int DepartementId { get; set; }
		public string Name { get; set; }= null!;
		public string StudentServiceNumber { get; set; } = null!;
		public string ProfHeadName { get; set; } = null!;

		// navigation proprites
		public int FacultyId { get; set; }
		public virtual Faculty Faculty { get; set; }
		public ICollection<AcadimicYear> AcadimicYears { get; set; }
		public virtual ICollection<Student> Students { get; set; }
		public virtual ICollection<Professor> Professors { get; set; }
		public virtual ICollection<Instructor>Instructors { get; set; }
		public virtual ICollection<CourseCategory> CoursesCategories { get; set;} 
	}
}