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

namespace Application.CQRS.Query.Professors
{
	public class GetAllProfessorsHandler : IQueryHandler<GetAllProfessorsQuery, List<ReturnedProfessorDto>>
	{
		private readonly IUnitOfwork unitOfwork;

		public GetAllProfessorsHandler(IUnitOfwork unitOfwork)
		{
			this.unitOfwork = unitOfwork;
		}

		public async Task<Result<List<ReturnedProfessorDto>>> Handle(GetAllProfessorsQuery request, CancellationToken cancellationToken)
		{
			try
			{
				IEnumerable<User> users = await unitOfwork.UserRepository.GetAllProfessorsAsync();
				List<ReturnedProfessorDto> returnedProfessorDtos = new List<ReturnedProfessorDto>();
				foreach (User user in users)
				{
					returnedProfessorDtos.Add(user.ConvertProfessorToReutrnedProfessorDto());
				}
				return Result.Success<List<ReturnedProfessorDto>> (returnedProfessorDtos);
			}
			catch(Exception ex)
			{
				return Result.Failure<List<ReturnedProfessorDto>>(new Error(code: "GetAllProfessors", message: ex.Message.ToString()));
			}
		}
	}
}
