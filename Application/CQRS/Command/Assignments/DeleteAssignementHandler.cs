﻿using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Domain.Models;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Command.Assignements
{
    public class DeleteAssignementHandler : ICommandHandler<DeleteAssignementCommand, int>
    {

        private readonly IUnitOfwork unitOfwork;

        public DeleteAssignementHandler(IUnitOfwork unitOfwork)
        {
            this.unitOfwork = unitOfwork;
        }

        public async Task<Result<int>> Handle(DeleteAssignementCommand request, CancellationToken cancellationToken)
        {
            try
            {
                User user = await unitOfwork.UserRepository.GetUserByUserName(request.InstructorUserName);
                if (user is null)
                    return Result.Failure<int>(new Error(code: "Delete Assignement", message: "not valid data"));


                Assignment assignement = await unitOfwork.AssignementRepository.GetByIdAsync(request.Id);
                if (assignement is null)
                    Result.Failure<int>(new Error(code: "Delete Assignement", message: "No assignement has this Id"));


                bool IfInstructorHasAccessToSection = await unitOfwork.SectionRepository.CheckIfInstructorInSection(user.Id, SectionId: assignement.SectionId);
                if (!IfInstructorHasAccessToSection)
                    return Result.Failure<int>(new Error(code: "Delete Assignement", message: "user has no access to this section"));
                //if (
                //	 (assignement.FullMark != request.AssignementDto.FullMark) ||
                //	 (assignement.Name!=request.AssignementDto.Name) 
                //	) 
                //{
                //	return Result.Failure<int>(new Error(code: "Delete Assignement", message: "Data of the assignement is not the same in database"));
                //}

                bool IsDeleted = await unitOfwork.AssignementRepository.DeleteAsync(request.Id);

                if (IsDeleted)
                {
                    int NumOfTasks = await unitOfwork.SaveChangesAsync();
                    return Result.Success<int>(request.Id);
                }
                return Result.Failure<int>(new Error(code: "Delete Assignement", message: "Unable To Delete"));
            }
            catch (Exception ex)
            {
                return Result.Failure<int>(new Error(code: "Delete Assignement", message: ex.Message.ToString()));
            }
        }
    }
}
