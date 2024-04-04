using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Domain.Models;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Command.Professors
{
	public class UpdateProfessorByIdCommandHandler : ICommandHandler<UpdateProfessorByIdCommand>
	{
		private readonly IUnitOfwork unitOfwork;

		public UpdateProfessorByIdCommandHandler(IUnitOfwork unitOfwork)
		{
			this.unitOfwork = unitOfwork;
		}

		public async Task<Result> Handle(UpdateProfessorByIdCommand request, CancellationToken cancellationToken)
		{
			try
			{
				request.NewProfessorInfo.Id = request.Id;
				bool IsUpdated = await unitOfwork.ProfessorRepository.UpdateAsync(request.NewProfessorInfo.GetProfessor());
				if (IsUpdated)
				{
					int NumOfTasks = await unitOfwork.SaveChangesAsync();
					return Result.Success();
				}
				return Result.Failure(new Error ("UpdatedProfessorById" , "Uanble to Update Professor"));
			}
			catch (Exception ex)
			{
				return Result.Failure(new Error(code: "UpdateProfessor.Exception", message: ex.Message.ToString()));
			}
		}
	}
}
