using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Domain.Models;
using Domain.Shared;
using Domain.Enums;
using System.Threading.Tasks;

namespace Application.CQRS.Query.Assignements
{
    public class GetAllAssignementsOfSectionHandler : IQueryHandler<GetAllAssignementsOfSectionQuery, IEnumerable<Assignment>>
    {
        private readonly IUnitOfwork unitOfwork;
        public GetAllAssignementsOfSectionHandler(IUnitOfwork unitOfwork)
        {
            this.unitOfwork = unitOfwork;
        }

        public async Task<Result<IEnumerable<Assignment>>> Handle(GetAllAssignementsOfSectionQuery request, CancellationToken cancellationToken)
        {
            try
            {
                bool IfUserHasAccessToSection = false;  
                var User = await unitOfwork.UserRepository.GetUserByUserName(request.assignmentToAnyUserDto.UserName);

                if (User is null)
                    return Result.Failure<IEnumerable<Assignment>>(new Error(code: "GetAllAssignementsOfSectionQuery", message: "Invalid data"));

                if (request.assignmentToAnyUserDto.TypeOfUser == TypesOfUsers.Instructor)
                    IfUserHasAccessToSection = await unitOfwork.SectionRepository.CheckIfInstructorInSection(SectionId: request.assignmentToAnyUserDto.SectionId, InstrucotrId: User.Id);
                else if (request.assignmentToAnyUserDto.TypeOfUser == TypesOfUsers.Professor)
                    IfUserHasAccessToSection = await unitOfwork.SectionRepository.CheckIfProfessorInSection(SectionId: request.assignmentToAnyUserDto.SectionId, ProfessorId: User.Id);
                else if (request.assignmentToAnyUserDto.TypeOfUser == TypesOfUsers.Student)
                    IfUserHasAccessToSection =await unitOfwork.StudentSectionRepository.CheckIfStudentInSection(StudentId: User.Id, SectionId: request.assignmentToAnyUserDto.SectionId);

                    if (! IfUserHasAccessToSection)
                    return Result.Failure<IEnumerable<Assignment>>(new Error(code: "GetAllAssignementsOfSectionQuery", message: "Has no access"));

                var AssignemntsOfSection = await unitOfwork.AssignementRepository.GetAllAssignementsOfSection(sectionId: request.assignmentToAnyUserDto.SectionId);   

                return Result.Success(AssignemntsOfSection);
            }
            catch (Exception ex)
            {
                return Result.Failure<IEnumerable<Assignment>>(new Error(code: "GetAllAssignementsOfSectionQuery", message: ex.Message.ToString()));
            }
        }
    }
}
