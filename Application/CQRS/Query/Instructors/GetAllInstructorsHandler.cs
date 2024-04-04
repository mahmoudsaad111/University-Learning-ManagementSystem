using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Application.CQRS.Query.Instructors;
using Contract;
using Contract.Dto.ReturnedDtos;
using Domain.Models;
using Domain.Shared;
 
namespace Application.CQRS.Query.Instructors
{
	public class GetAllInstructorsHandler : IQueryHandler<GetAllInstructorsQuery, List<ReturnedInstructorDto>>
	{
		private readonly IUnitOfwork unitOfwork;

		public GetAllInstructorsHandler(IUnitOfwork unitOfwork)
		{
			this.unitOfwork = unitOfwork;
		}

		public async Task<Result<List<ReturnedInstructorDto>>> Handle(GetAllInstructorsQuery request, CancellationToken cancellationToken)
		{
			try
			{
				IEnumerable<User> users = await unitOfwork.UserRepository.GetAllInstructorsAsync();
				List<ReturnedInstructorDto> returnedInstructorDtos = new List<ReturnedInstructorDto>();
				foreach (User user in users)
				{
					returnedInstructorDtos.Add(user.ConvertInstructorToReutrnedInsructorDto());
				}
				return Result.Success<List<ReturnedInstructorDto>> (returnedInstructorDtos);
			}
			catch(Exception ex)
			{
				return Result.Failure<List<ReturnedInstructorDto>>(new Error(code: "GetAllInstructors", message: ex.Message.ToString()));
			}
		}
	}
}
