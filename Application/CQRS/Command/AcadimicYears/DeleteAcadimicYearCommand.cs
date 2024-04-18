using Application.Common.Interfaces.CQRSInterfaces; 
using Contract.Dto.AcadimicYears;
 
 
namespace Application.CQRS.Command.AcadimicYears
{
	public class DeleteAcadimicYearCommand : ICommand<int>
	{
		public int Id { get; set; }	
		 
	}
}
