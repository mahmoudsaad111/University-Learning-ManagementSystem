using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Dto.StudentCourseCycles
{
    public class StudentCourseCycleDto
    {
        public int StudentId { get; set; }
        public int CourseCycleId { get; set; }  
         
        public StudentCourseCycle GetStudentCourseCycle()
        {
            return new StudentCourseCycle
            {
                StudentId = StudentId,
                CourseCycleId = CourseCycleId
            };
        }
    }
}
