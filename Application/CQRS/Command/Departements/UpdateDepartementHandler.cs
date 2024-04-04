using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Domain.Models;
using Domain.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Command.Departements
{
	public class UpdateDepartementHandler : ICommandHandler<UpdateDepartementCommand, Departement>
	{
		private readonly IUnitOfwork unitOfwork; 
		public UpdateDepartementHandler (IUnitOfwork unitOfwork)
		{
			this.unitOfwork = unitOfwork;
		}
		public async Task<Result<Departement>> Handle(UpdateDepartementCommand request, CancellationToken cancellationToken)
		{
			try
			{
				Departement departement = unitOfwork.DepartementRepository.Find(f => f.DepartementId == request.Id);
				if (departement is null)
					return Result.Failure<Departement>(new Error(code: "Update Departement", message: "No Departement exist by this Id"));
				
				departement.FacultyId = request.departementDto.FacultyId;
				departement.StudentServiceNumber = request.departementDto.StudentServiceNumber;
				departement.Name = request.departementDto.Name;
				departement.ProfHeadName = request.departementDto.ProfHeadName;

				int NumOfTasks  = await unitOfwork.SaveChangesAsync();
				return Result.Success<Departement>(departement);
			}
			catch (Exception ex)
			{
				return Result.Failure<Departement>(new Error(code: "Updated Departement" , message: ex.Message.ToString())); 
			}
		}
	}
}
