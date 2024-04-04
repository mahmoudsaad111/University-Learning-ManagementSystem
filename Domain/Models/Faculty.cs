
namespace Domain.Models
{
	public class Faculty
	{
		public int FacultyId { get; set; }
		public string Name { get; set; }
		public string StudentServiceNumber { get; set; } = null!;
		public int NumOfYears { get; set; }
		public string ProfHeadName { get; set; } = null!;

		// navigartion proprties
		public virtual ICollection<Departement> Departements { get; set; }
	}
}