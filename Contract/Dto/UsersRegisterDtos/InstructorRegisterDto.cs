
using Domain.Models;

namespace Contract.Dto.UsersRegisterDtos
{
    public class InstructorRegisterDto : UserRegisterDto
    {
        public string? Specification { get; set; }
        public int DepartementId {get;set;}
        public Instructor GetInstructor( )
        {
            return new Instructor
            {
                InstructorId=Id, 
                Specification = Specification,
                DepartementId = DepartementId == 0 ? 1 : DepartementId,
            };
        }

    }
}