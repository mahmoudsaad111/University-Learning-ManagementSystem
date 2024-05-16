using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Domain.Models;
using Domain.Shared;


namespace Application.CQRS.Command.Assignements
{
    public class UpdateDepartementHandler : ICommandHandler<UpdateAssignementCommand, Assignment>
    {
        private readonly IUnitOfwork unitOfwork;
        public UpdateDepartementHandler(IUnitOfwork unitOfwork)
        {
            this.unitOfwork = unitOfwork;
        }
        public async Task<Result<Assignment>> Handle(UpdateAssignementCommand request, CancellationToken cancellationToken)
        {
            try
            {
                User user = await unitOfwork.UserRepository.GetUserByUserName(request.InstructorUserName);
                if (user is null)
                    return Result.Failure<Assignment>(new Error(code: "Update Assignement", message: "not valid data"));

                Assignment assignement = await unitOfwork.AssignementRepository.GetByIdAsync(request.Id);
                if (assignement is null)
                    return Result.Failure<Assignment>(new Error(code: "Update Assignement", message: "No Assignement exist by this Id"));

                bool IfInstructorHasAccessToSection = await unitOfwork.SectionRepository.CheckIfInstructorInSection(user.Id, SectionId: assignement.SectionId);
                if (!IfInstructorHasAccessToSection)
                    return Result.Failure<Assignment>(new Error(code: "Update Assignement", message: "user has no access to this section"));

                if (assignement.SectionId != request.AssignementDto.SectionId)
                    return Result.Failure<Assignment>(new Error(code: "Update Assignement", message: "Can not change the SectionId"));

                if (request.AssignementDto.EndedAt <= DateTime.Now)
                    return Result.Failure<Assignment>(new Error(code: "Update Assignement", message: "Can not change the DeadLine to this date"));


                assignement.Name = request.AssignementDto.Name;
                assignement.Description = request.AssignementDto.Description;
                assignement.FullMark = request.AssignementDto.FullMark;
                assignement.EndedAt = request.AssignementDto.EndedAt;

                int NumOfTasks = await unitOfwork.SaveChangesAsync();
                return Result.Success<Assignment>(assignement);
            }
            catch (Exception ex)
            {
                return Result.Failure<Assignment>(new Error(code: "Updated Assignement", message: ex.Message.ToString()));
            }
        }
    }
}
