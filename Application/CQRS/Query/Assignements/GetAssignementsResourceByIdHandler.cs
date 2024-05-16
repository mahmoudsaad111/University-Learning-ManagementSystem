using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Contract.Dto.AssignementAnswers;
using Contract.Dto.Assignements;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Query.Assignements
{
    public class GetAssignementsResourceByIdHandler : IQueryHandler<GetAssignementsResourceByIdQuery, AssignemntFilesDto>
    {
        private readonly IUnitOfwork unitOfwork;

        public GetAssignementsResourceByIdHandler(IUnitOfwork unitOfwork)
        {
            this.unitOfwork = unitOfwork;
        }

        public async Task<Result<AssignemntFilesDto>> Handle(GetAssignementsResourceByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                bool IfUserHasAccessToSection = false;
                var User = await unitOfwork.UserRepository.GetUserByUserName(request.ProfessorOrInstrucotrUserName);

                if (User is null)
                    return Result.Failure<AssignemntFilesDto>(new Error(code: "GetAssignementsResourceByIdQuery", message: "Invalid data"));

                var Assignment = await unitOfwork.AssignementRepository.GetByIdAsync(request.AssignmentId);
                if (Assignment is null)
                    return Result.Failure<AssignemntFilesDto>(new Error(code: "GetAssignementsResourceByIdQuery", message: "Invalid data"));


                if (request.IsInstructor)
                    IfUserHasAccessToSection = await unitOfwork.SectionRepository.CheckIfInstructorInSection(SectionId: Assignment.SectionId, InstrucotrId: User.Id);
                else
                    IfUserHasAccessToSection = await unitOfwork.SectionRepository.CheckIfProfessorInSection(SectionId: Assignment.SectionId, ProfessorId: User.Id);

                if (!IfUserHasAccessToSection)
                    return Result.Failure<AssignemntFilesDto>(new Error(code: "GetAssignementsResourceByIdQuery", message: "Has no access"));


                var AssignmentFiles = await unitOfwork.AssignementResourceRepository.GetFilesOfAssignemnt(assignemntId: request.AssignmentId);
                return Result.Success(AssignmentFiles); 
            }
            catch (Exception ex)
            {
                return Result.Failure<AssignemntFilesDto>(new Error(code: "GetAssignementsResourceByIdQuery", message: ex.Message.ToString()));
            }
        }
    }
}
