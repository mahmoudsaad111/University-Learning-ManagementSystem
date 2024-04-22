 
namespace Domain.Models
{
    public class AcadimicYear
    {
        public int AcadimicYearId { get; set; }
        public int Year {  get; set; }
        public int DepartementId { get; set; }  
        public Departement Departement { get; set; }    
        public ICollection<Course> Courses { get; set; }
        public ICollection<Group> Groups { get; set; }
        public ICollection<Student> Students { get; set; }
    }
}

 