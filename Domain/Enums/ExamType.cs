using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public enum ExamType
    {
        Quiz, // for sections or CourseCycle
        Midterm, // for courseCycles only
        Semester, // for course -academicYear
        Final  // for course-academicYear
    }
}
