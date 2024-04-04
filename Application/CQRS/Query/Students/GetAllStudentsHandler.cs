using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Contract;
using Contract.Dto.ReturnedDtos;
using Domain.Models;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Query.Students
{
	public class GetAllProfessorsHandler : IQueryHandler<GetAllStudentsQuery, List<ReturnedStudentDto>>
	{
		private readonly IUnitOfwork unitOfwork;

		public GetAllProfessorsHandler(IUnitOfwork unitOfwork)
		{
			this.unitOfwork = unitOfwork;
		}

		public async Task<Result<List<ReturnedStudentDto>>> Handle(GetAllStudentsQuery request, CancellationToken cancellationToken)
		{
			try
			{
				IEnumerable<User> users = await unitOfwork.UserRepository.GetAllStudentsAsync();
				List<ReturnedStudentDto> returnedStudentDtos = new List<ReturnedStudentDto>();
				foreach (User user in users)
				{
					returnedStudentDtos.Add(user.ConvertStudnetToReutrnedStudentDto());
				}
				return Result.Success<List<ReturnedStudentDto>> (returnedStudentDtos);
			}
			catch(Exception ex)
			{
				return Result.Failure<List<ReturnedStudentDto>>(new Error(code: "GetAllStudents", message: ex.Message.ToString()));
			}
		}
	}
}
