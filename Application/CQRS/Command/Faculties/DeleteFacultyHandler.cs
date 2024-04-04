using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Domain.Models;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Command.Faculties
{
	public class DeleteFacultyHandler : ICommandHandler<DeleteFacultyCommand, int>
	{

		private readonly IUnitOfwork unitOfwork;

		public DeleteFacultyHandler(IUnitOfwork unitOfwork)
		{
			this.unitOfwork = unitOfwork;
		}

		public async Task<Result<int>> Handle(DeleteFacultyCommand request, CancellationToken cancellationToken)
		{
			try
			{
				Faculty faculty = await unitOfwork.FacultyRepository.GetByIdAsync(request.Id);
				if (faculty == null)
					Result.Failure<int>(new Error(code: "Delete Faculty", message: "No faculty has this Id")) ;

				if ((faculty.NumOfYears != request.FacultyDto.NumOfYears) ||
					 (faculty.ProfHeadName != request.FacultyDto.ProfHeadName) ||
					 (faculty.StudentServiceNumber != request.FacultyDto.StudentServiceNumber)
					) 
				{
					return Result.Failure<int>(new Error(code: "Delete Faculty", message: "Data of the faculty is not the same in database"));
				}

				bool IsDeleted = await unitOfwork.FacultyRepository.DeleteAsync(request.Id);

				if (IsDeleted) 
				{
					int NumOfTasks = await unitOfwork.SaveChangesAsync();
					return Result.Success<int>(request.Id);
				}
				return Result.Failure<int>(new Error(code: "Delete Faculty", message: "Unable To Delete")) ;
			}
			catch(Exception ex) 
			{
				return Result.Failure<int>(new Error(code: "Delete Faculty", message: ex.Message.ToString()));
			}
		}
	}
}
