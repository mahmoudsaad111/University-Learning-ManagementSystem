using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Domain.Models;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Command.Students
{
	public class UpdateProfessorByIdCommandHandler : ICommandHandler<UpdateStudentByIdCommand>
	{
		private readonly IUnitOfwork unitOfwork;

		public UpdateProfessorByIdCommandHandler(IUnitOfwork unitOfwork)
		{
			this.unitOfwork = unitOfwork;
		}

		public async Task<Result> Handle(UpdateStudentByIdCommand request, CancellationToken cancellationToken)
		{
			try
			{
				request.NewStudentInfo.Id = request.Id;
				bool IsUpdated = await unitOfwork.StudentRepository.UpdateAsync(request.NewStudentInfo.GetStudent());
				if (IsUpdated)
				{
					int NumOfTasks = await unitOfwork.SaveChangesAsync();
					return Result.Success();
				}
				return Result.Failure(new Error ("UpdatedStudentById" , "Uanble to Update Student"));
			}
			catch (Exception ex)
			{
				return Result.Failure(new Error(code: "UpdateStudent.Exception", message: ex.Message.ToString()));
			}
		}
	}
}
