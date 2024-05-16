using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Contract.Dto.AssignementAnswers;
using Domain.Models;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Query.AssignementAnswers
{
    public class GetAllStudnetWithAnswersOnAssignementHandler : IQueryHandler<GetAllStudnetWithAnswersOnAssignementQuery, IEnumerable<StudentHasAnswerOfAssignemet>>
    {
        private readonly IUnitOfwork unitOfwork;
        public GetAllStudnetWithAnswersOnAssignementHandler(IUnitOfwork unitOfwork)
        {
            this.unitOfwork = unitOfwork;
        }
        public async Task<Result<IEnumerable<StudentHasAnswerOfAssignemet>>> Handle(GetAllStudnetWithAnswersOnAssignementQuery request, CancellationToken cancellationToken)
        {
            try
            {
                bool IfUserHasAccessToSection = false;
                var User = await unitOfwork.UserRepository.GetUserByUserName(request.ProfOrInstUserName);

                if (User is null)
                    return Result.Failure<IEnumerable<StudentHasAnswerOfAssignemet>>(new Error(code: "GetAllStudnetWithAnswersOnAssignement", message: "Invalid data"));

                var Assignment= await unitOfwork.AssignementRepository.GetByIdAsync(request.AssignemntId);
                if(Assignment is null)
                    return Result.Failure<IEnumerable<StudentHasAnswerOfAssignemet>>(new Error(code: "GetAllStudnetWithAnswersOnAssignement", message: "Invalid data"));


                if (request.IsInstructor)
                    IfUserHasAccessToSection = await unitOfwork.SectionRepository.CheckIfInstructorInSection(SectionId: Assignment.SectionId, InstrucotrId: User.Id);
                else
                    IfUserHasAccessToSection = await unitOfwork.SectionRepository.CheckIfProfessorInSection(SectionId: Assignment.SectionId, ProfessorId: User.Id);

                if (!IfUserHasAccessToSection)
                    return Result.Failure<IEnumerable<StudentHasAnswerOfAssignemet>>(new Error(code: "GetAllStudnetWithAnswersOnAssignement", message: "Has no access"));

                var AsnwersOfAssignment = await unitOfwork.AssignementAnswerRepository.GetAllStudentWithAnswersOnAssignemnt(assignemntId: request.AssignemntId);
                return Result.Success(AsnwersOfAssignment);
            }
            catch (Exception ex)
            {
                return Result.Failure<IEnumerable<StudentHasAnswerOfAssignemet>>(new Error(code: "GetAllStudnetWithAnswersOnAssignement", message: ex.Message.ToString()));
            }
        }
    }
}
