using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Application.CQRS.Command.AssignementAnswers;
using Domain.Models;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Command.AssignementAnswers
{
    public class CreateAssignementAnswerHandler : ICommandHandler<CreateAssignementAnswerCommand, AssignmentAnswer>
    {
        private readonly IUnitOfwork unitOfwork;

        public CreateAssignementAnswerHandler(IUnitOfwork unitOfwork)
        {
            this.unitOfwork = unitOfwork;
        }
        public async Task<Result<AssignmentAnswer>> Handle(CreateAssignementAnswerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                User user = await unitOfwork.UserRepository.GetUserByUserName(request.StudentUserName);
                if (user is null || user.Id !=request.AssignementAnswerDto.StudentId)
                    return Result.Failure<AssignmentAnswer>(new Error(code: "Create AssignementAnswer", message: "not valid data"));

                Assignment assignment = await unitOfwork.AssignementRepository.GetByIdAsync(request.AssignementAnswerDto.AssignementId);
                if (assignment is null)
                    return Result.Failure<AssignmentAnswer>(new Error(code: "Create AssignementAnswer", message: "not valid data"));

                if (assignment.EndedAt < DateTime.Now)
                    return Result.Failure<AssignmentAnswer>(new Error(code: "Create AssignementAnswer", message: "Can not submit solutions : DeadLine expired"));

                bool IfStudentHasAccessToAssignment = await unitOfwork.StudentSectionRepository.CheckIfStudentInSection(StudentId: user.Id, SectionId: assignment.SectionId);
                if (!IfStudentHasAccessToAssignment)
                    return Result.Failure<AssignmentAnswer>(new Error(code: "Create AssignementAnswer", message: "Has no access"));

                // important step 
                request.AssignementAnswerDto.StudentId = user.Id;
       
                AssignmentAnswer assignementanswer = await unitOfwork.AssignementAnswerRepository.CreateAsync(request.AssignementAnswerDto.GetAssignementAnswer());
                if (assignementanswer is null)
                    return Result.Failure<AssignmentAnswer>(new Error(code: "Create AssignementAnswer", message: "not valid data"));

                await unitOfwork.SaveChangesAsync();
                return Result.Success<AssignmentAnswer>(assignementanswer);
            }
            catch (Exception ex)
            {
                return Result.Failure<AssignmentAnswer>(new Error(code: "Create AssignementAnswer", message: ex.Message.ToString()));
            }
        }
    }
}
