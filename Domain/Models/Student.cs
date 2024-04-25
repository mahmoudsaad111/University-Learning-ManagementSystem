
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{

	// url : host /1/1/2/3
	public class Student
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int StudentId { get; set; }
		 
		public double GPA { get; set; }

		//-- navigation properties
		public User User { get; set; }	
		public int GroupId { get; set; }
		public Group Group { get; set; }
		public int DepartementId { get; set; }
		public Departement Departement { get; set; }

        public int AcadimicYearId { get; set; }
		public AcadimicYear AcadimicYear { get; set; }	
      //  public ICollection<StudentSection> StudentsInSection { get; set; }

		public ICollection<AssignmentAnswer> AssignmentAnswers { get; set; }

		public ICollection<ExamAnswer> ExamAnswers { get; set; }
		
		// for student attendence in lectures
        
		public ICollection<Student_Lecture> StudentLecture { get; set;  }

		public ICollection<StudentNote> StudentNotes { get; set; }	

		public ICollection<StudentCourseCycle> CourseCycleOfStudent {  get; set; }	

	}
}