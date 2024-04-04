using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Domain.Models;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Command.Instructors
{
	public class UpdateInstructorByIdCommandHandler : ICommandHandler<UpdateInstructorByIdCommand>
	{
		private readonly IUnitOfwork unitOfwork;

		public UpdateInstructorByIdCommandHandler(IUnitOfwork unitOfwork)
		{
			this.unitOfwork = unitOfwork;
		}

		public async Task<Result> Handle(UpdateInstructorByIdCommand request, CancellationToken cancellationToken)
		{
			try
			{
				request.NewInstructorInfo.Id = request.Id;
				bool IsUpdated = await unitOfwork.InstructorRepository.UpdateAsync(request.NewInstructorInfo.GetInstructor());
				if (IsUpdated)
				{
					int NumOfTasks = await unitOfwork.SaveChangesAsync();
					return Result.Success();
				}
				return Result.Failure(new Error ("UpdatedInstructorById" , "Uanble to Update Instructor"));
			}
			catch (Exception ex)
			{
				return Result.Failure(new Error(code: "UpdateInstructor.Exception", message: ex.Message.ToString()));
			}
		}
	}
}
