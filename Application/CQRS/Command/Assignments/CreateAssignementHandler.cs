using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Application.CQRS.Command.Assignements;
using Domain.Models;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Command.Assignements
{
    public class CreateAssignementHandler : ICommandHandler<CreateAssignementCommand, Assignment>
    {
        private readonly IUnitOfwork unitOfwork;

        public CreateAssignementHandler(IUnitOfwork unitOfwork)
        {
            this.unitOfwork = unitOfwork;
        }

        public async Task<Result<Assignment>> Handle(CreateAssignementCommand request, CancellationToken cancellationToken)
        {
            try
            {
                User user = await unitOfwork.UserRepository.GetUserByUserName(request.InstructorUserName);
                if (user is null)
                    return Result.Failure<Assignment>(new Error(code: "Create Assignement", message: "not valid data"));
                
                bool IfInstructorHasAccessToSection = await unitOfwork.SectionRepository.CheckIfInstructorInSection(user.Id, SectionId: request.AssignementDto.SectionId);
                if (!IfInstructorHasAccessToSection)
                    return Result.Failure<Assignment>(new Error(code: "Create Assignement", message: "user has no access to this section"));

                if (request.AssignementDto.EndedAt <= DateTime.Now)
                    return Result.Failure<Assignment>(new Error(code: "Create Assignement", message: "Invalid EndTime Date"));


                Assignment assignement = await unitOfwork.AssignementRepository.CreateAsync(request.AssignementDto.GetAssignement());
                if (assignement is null)
                    return Result.Failure<Assignment>(new Error(code: "Create Assignement", message: "not valid data"));

              

                await unitOfwork.SaveChangesAsync();
                return Result.Success<Assignment>(assignement);
            }
            catch (Exception ex)
            {
                return Result.Failure<Assignment>(new Error(code: "Create Assignement", message: ex.Message.ToString()));
            }
        }
    }
}
