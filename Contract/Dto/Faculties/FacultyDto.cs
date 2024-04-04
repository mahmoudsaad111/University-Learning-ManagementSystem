using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Dto.Faculties
{
    public class FacultyDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string StudentServiceNumber { get; set; }
        [Required]
        public int NumOfYears { get; set; }
        [Required]
        public string ProfHeadName { get; set; }
        public Faculty GetFaculty()
        {
            return new Faculty
            {
                Name = Name,
                StudentServiceNumber = StudentServiceNumber,
                NumOfYears = NumOfYears,
                ProfHeadName = ProfHeadName
            };
        }
    }
}
