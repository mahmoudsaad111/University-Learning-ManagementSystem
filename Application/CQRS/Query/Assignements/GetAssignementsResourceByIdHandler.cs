using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Contract.Dto.AssignementAnswers;
using Contract.Dto.Assignements;
using Domain.Enums;
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
                var User = await unitOfwork.UserRepository.GetUserByUserName(request.assignmentsResourseToAnyUserDto.UserName);

                if (User is null)
                    return Result.Failure<AssignemntFilesDto>(new Error(code: "GetAssignementsResourceByIdQuery", message: "Invalid data"));

                var Assignment = await unitOfwork.AssignementRepository.GetByIdAsync(request.assignmentsResourseToAnyUserDto.AssignmentId);
                if (Assignment is null)
                    return Result.Failure<AssignemntFilesDto>(new Error(code: "GetAssignementsResourceByIdQuery", message: "Invalid data"));


                if (request.assignmentsResourseToAnyUserDto.TypeOfUser == TypesOfUsers.Instructor)
                    IfUserHasAccessToSection = await unitOfwork.SectionRepository.CheckIfInstructorInSection(SectionId: Assignment.SectionId, InstrucotrId: User.Id);
                else if (request.assignmentsResourseToAnyUserDto.TypeOfUser == TypesOfUsers.Professor)
                    IfUserHasAccessToSection = await unitOfwork.SectionRepository.CheckIfProfessorInSection(SectionId: Assignment.SectionId, ProfessorId: User.Id);
                else if (request.assignmentsResourseToAnyUserDto.TypeOfUser == TypesOfUsers.Student)
                    IfUserHasAccessToSection = await unitOfwork.StudentSectionRepository.CheckIfStudentInSection(StudentId: User.Id, SectionId: Assignment.SectionId);


                if (!IfUserHasAccessToSection)
                    return Result.Failure<AssignemntFilesDto>(new Error(code: "GetAssignementsResourceByIdQuery", message: "Has no access"));

                var AssignmentFiles = await unitOfwork.AssignementResourceRepository.GetFilesOfAssignemnt(assignemntId: Assignment.AssignmentId);
                return Result.Success(AssignmentFiles); 
            }
            catch (Exception ex)
            {
                return Result.Failure<AssignemntFilesDto>(new Error(code: "GetAssignementsResourceByIdQuery", message: ex.Message.ToString()));
            }
        }
    }
}
