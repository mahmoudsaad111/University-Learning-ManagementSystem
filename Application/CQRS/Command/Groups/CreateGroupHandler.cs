using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Domain.Models;
using Domain.Shared;
 
namespace Application.CQRS.Command.Groups
{
	public class CreateGroupHandler : ICommandHandler<CreateGroupCommand, Group>
	{
		private readonly IUnitOfwork unitOfwork;

		public CreateGroupHandler(IUnitOfwork unitOfwork)
		{
			this.unitOfwork = unitOfwork;
		}

		public async Task<Result<Group>> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
		{
			try
			{
                Result<int> result = await unitOfwork.AcadimicYearRepository.GetAcadimicYearIdByDepIdAndYearNum(request.groupDto.DepartementId, request.groupDto.AcadimicYear);
                if (result.IsFailure)
                    return Result.Failure<Group>(new Error(code: "Group Course", message: "not valid data"));

                request.groupDto.AcadimicYear = result.Value;

                Group group =await unitOfwork.GroupRepository.CreateAsync(request.groupDto.GetGroup()) ;
				if (group is null)
					return Result.Failure<Group>(new Error(code: "Create Group" ,message :"not valid data" ));

				await unitOfwork.SaveChangesAsync();
				return Result.Success<Group>(group);
			}
			catch(Exception ex) 
			{
				return Result.Failure<Group>(new Error(code: "Create Group", message: ex.Message.ToString())) ;
			}
		}
	}
}
