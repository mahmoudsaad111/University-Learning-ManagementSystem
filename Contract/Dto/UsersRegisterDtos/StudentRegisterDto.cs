using System.ComponentModel.DataAnnotations;
using Domain.Models;
namespace Contract.Dto.UsersRegisterDtos
{
    public class StudentRegisterDto : UserRegisterDto
    {
      
        [Required]
        public double GPA { get; set; } 
        [Required]
        public int AcadimicYearId { get; set; }
        [Required]
        public int GroupId {  get; set; }
        [Required]
        public int DepartementId {  get; set; }
        public Student  GetStudent()
        {
            return new Student()
            {
                StudentId = Id,
                GPA = GPA,
                AcadimicYearId = AcadimicYearId,
                GroupId = GroupId == 0 ? 1 : GroupId,
                DepartementId = DepartementId == 0 ? 1 : DepartementId
            };
        }
    }
}