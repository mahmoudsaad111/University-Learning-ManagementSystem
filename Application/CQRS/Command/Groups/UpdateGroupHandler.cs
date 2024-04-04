using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Domain.Models;
using Domain.Shared;
 
namespace Application.CQRS.Command.Groups
{
	public class UpdateGroupHandler : ICommandHandler<UpdateGroupCommand, Group>
	{
		private readonly IUnitOfwork unitOfwork; 
		public UpdateGroupHandler (IUnitOfwork unitOfwork)
		{
			this.unitOfwork = unitOfwork;
		}
		public async Task<Result<Group>> Handle(UpdateGroupCommand request, CancellationToken cancellationToken)
		{
			try
			{
				Group group = unitOfwork.GroupRepository.Find(f => f.GroupId == request.Id);
				if (group is null)
					return Result.Failure<Group>(new Error(code: "Update Group", message: "No group exist by this Id"));


                Result<int> result = await unitOfwork.AcadimicYearRepository.GetAcadimicYearIdByDepIdAndYearNum(request.GroupDto.DepartementId, request.GroupDto.AcadimicYear);
                if (result.IsFailure)
                    return Result.Failure<Group>(new Error(code: "Group Course", message: "not valid data"));



				group.AcadimicYearId = result.Value;
				group.Name = request.GroupDto.Name;
				group.StudentHeadName = request.GroupDto.StudentHeadName;
				group.StudentHeadPhone= request.GroupDto.StudentHeadPhone;

				int NumOfTasks  = await unitOfwork.SaveChangesAsync();
				return Result.Success<Group>(group);
			}
			catch (Exception ex)
			{
				return Result.Failure<Group>(new Error(code: "Updated Group" , message: ex.Message.ToString())); 
			}
		}
	}
}
