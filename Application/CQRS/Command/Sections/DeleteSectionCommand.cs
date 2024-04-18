using Application.Common.Interfaces.CQRSInterfaces;
 
using Contract.Dto.Sections;

namespace Application.CQRS.Command.Sections
{
	public class DeleteSectionCommand : ICommand<int>
	{
		public int Id { get; set; }	
		//public SectionDto SectionDto { get; set; }
	}
}
