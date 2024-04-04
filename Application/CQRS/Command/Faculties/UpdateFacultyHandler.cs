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

namespace Application.CQRS.Command.Faculties
{
	public class UpdateDepartementHandler : ICommandHandler<UpdateFacultyCommand, Faculty>
	{
		private readonly IUnitOfwork unitOfwork; 
		public UpdateDepartementHandler (IUnitOfwork unitOfwork)
		{
			this.unitOfwork = unitOfwork;
		}
		public async Task<Result<Faculty>> Handle(UpdateFacultyCommand request, CancellationToken cancellationToken)
		{
			try
			{
				Faculty faculty = unitOfwork.FacultyRepository.Find(f => f.FacultyId == request.Id);
				if (faculty is null)
					return Result.Failure<Faculty>(new Error(code: "Update Faculty", message: "No Faculty exist by this Id"));
				
				faculty.NumOfYears = request.facultyDto.NumOfYears;
				faculty.StudentServiceNumber = request.facultyDto.StudentServiceNumber;
				faculty.Name = request.facultyDto.Name;
				faculty.ProfHeadName = request.facultyDto.ProfHeadName;

				int NumOfTasks  = await unitOfwork.SaveChangesAsync();
				return Result.Success<Faculty>(faculty);
			}
			catch (Exception ex)
			{
				return Result.Failure<Faculty>(new Error(code: "Updated Faculty" , message: ex.Message.ToString())); 
			}
		}
	}
}
