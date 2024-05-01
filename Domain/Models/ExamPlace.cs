using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class ExamPlace
    {
        public int ExamPlaceId { get; set; }  

        [Required]
        public ExamType ExamType { get; set; }

        public int? SectionId { get; set; } // Nullable for non-Section exam types
        public int? CourseCycleId { get; set; } // Nullable for non-CourseCycle exam types
      //  public int? AcadimicYearId { get; set; } // Nullable for non-Mid/Final exam types
        public int? CourseId { get; set; } // Nullable for non-Mid/Final exam types
        public Section Section { get; set; }
        public CourseCycle CourseCycle { get; set; }
      //  public AcadimicYear AcademicYear { get; set;  } Cours Has Acadimic Year
        public Course Course { get; set; }

        public ICollection<Exam> ExamsInExamPlace { get; set; }
    }

}
