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

namespace Application.CQRS.Command.AcadimicYears
{
	public class UpdateAcadimicYearHandler : ICommandHandler<UpdateAcadimicYearCommand, AcadimicYear>
	{
		private readonly IUnitOfwork unitOfwork; 
		public UpdateAcadimicYearHandler (IUnitOfwork unitOfwork)
		{
			this.unitOfwork = unitOfwork;
		}
		public async Task<Result<AcadimicYear>> Handle(UpdateAcadimicYearCommand request, CancellationToken cancellationToken)
		{
			try
			{
				AcadimicYear AcadimicYear = unitOfwork.AcadimicYearRepository.Find(f => f.AcadimicYearId == request.Id);
				if (AcadimicYear is null)
					return Result.Failure<AcadimicYear>(new Error(code: "Update AcadimicYear", message: "No AcadimicYear exist by this Id"));

				AcadimicYear.Year = request.AcadimicYearDto.Year; 
				AcadimicYear.DepartementId = request.AcadimicYearDto.DepartementId;
				
				int NumOfTasks  = await unitOfwork.SaveChangesAsync();
				return Result.Success<AcadimicYear>(AcadimicYear);
			}
			catch (Exception ex)
			{
				return Result.Failure<AcadimicYear>(new Error(code: "Updated AcadimicYear" , message: ex.Message.ToString())); 
			}
		}
	}
}
