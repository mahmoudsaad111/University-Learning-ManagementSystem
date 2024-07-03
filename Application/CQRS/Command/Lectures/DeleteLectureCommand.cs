using Application.Common.Interfaces.CQRSInterfaces;
using Contract.Dto.Lectures;

namespace Application.CQRS.Command.Lectures
{
    public class DeleteLectureCommand : ICommand<int>
	{
		public int Id { get; set; }	
		public string CreatorUserName { get; set; }
        //public LectureDto LectureDto { get; set; }
    }
}
