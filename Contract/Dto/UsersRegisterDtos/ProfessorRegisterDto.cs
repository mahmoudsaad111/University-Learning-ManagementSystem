using Domain.Models;
namespace Contract.Dto.UsersRegisterDtos
{
    public class ProfessorRegisterDto : UserRegisterDto
    {
        public string? Specification { get; set; }
        public int DepartementId { get; set; }
        public Professor GetProfessor( )
        {
            return new Professor
            {
                ProfessorId=Id,
                Specification = Specification,
                DepartementId = DepartementId == 0 ? 1 : DepartementId,
            };
        }
    }
}