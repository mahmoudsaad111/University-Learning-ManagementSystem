using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Domain.Models;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Query.Students
{
	public class GetStudentByIdHandler : IQueryHandler<GetStudentByIdQuery, Student>
	{
		private readonly IUnitOfwork unitOfwork;

		public GetStudentByIdHandler(IUnitOfwork unitOfwork)
		{
			this.unitOfwork = unitOfwork;
		}

		public async Task<Result<Student>> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
		{
			try
			{
				Student student =await  unitOfwork.StudentRepository.GetByIdAsync(request.Id);
				if (student is null)
					return Result.Failure<Student>(new Error(code: "GetStudentById",message: "There is no Student By this Id"));
				return Result.Success<Student>(student);
			}
			catch (Exception ex)
			{
				return Result.Failure<Student>(new Error (code :"GetStudent" , message: ex.Message.ToString()));
			}
		}
	}
}
