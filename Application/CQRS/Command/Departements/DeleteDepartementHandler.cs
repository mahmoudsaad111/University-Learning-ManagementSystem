using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Domain.Models;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Command.Departements
{
	public class DeleteDepartementHandler : ICommandHandler<DeleteDepartementCommand, int>
	{

		private readonly IUnitOfwork unitOfwork;

		public DeleteDepartementHandler(IUnitOfwork unitOfwork)
		{
			this.unitOfwork = unitOfwork;
		}

		public async Task<Result<int>> Handle(DeleteDepartementCommand request, CancellationToken cancellationToken)
		{
			try
			{
				Departement departement = await unitOfwork.DepartementRepository.GetByIdAsync(request.Id);
				if (departement == null)
					return Result.Failure<int>(new Error(code: "Delete Departement", message: "No departement has this Id")) ;

				if ((departement.FacultyId != request.DepartementDto.FacultyId) ||
					 (departement.ProfHeadName != request.DepartementDto.ProfHeadName) ||
					 (departement.StudentServiceNumber != request.DepartementDto.StudentServiceNumber) ||
					 (departement.Name!=request.DepartementDto.Name)
					) 
				{
					return Result.Failure<int>(new Error(code: "Delete Departement", message: "Data of the departement is not the same in database"));
				}

				bool IsDeleted = await unitOfwork.DepartementRepository.DeleteAsync(request.Id);

				if (IsDeleted) 
				{
					int NumOfTasks = await unitOfwork.SaveChangesAsync();
					return Result.Success<int>(request.Id);
				}
				return Result.Failure<int>(new Error(code: "Delete Departement", message: "Unable To Delete")) ;
			}
			catch(Exception ex) 
			{
				return Result.Failure<int>(new Error(code: "Delete Departement", message: ex.Message.ToString()));
			}
		}
	}
}
