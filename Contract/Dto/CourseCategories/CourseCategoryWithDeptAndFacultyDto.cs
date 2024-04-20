using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Dto.CourseCategories
{
    public class CourseCategoryWithDeptAndFacultyDto
    {
        public int CourseCategoryId { get; set; }
        public string CourseCategoryName { get; set; }
        public string CourseCategoryDescription { get; set; }
        public int DepartementId { get; set; }
        public string DepartementName { get; set; }
        public int FacultyId { get; set; }  
        public string FacultyName { get; set; }

    }
}
