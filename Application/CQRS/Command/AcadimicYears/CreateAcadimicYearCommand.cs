using Application.Common.Interfaces.CQRSInterfaces;
using Contract.Dto.AcadimicYears;
using Domain.Models;
 
namespace Application.CQRS.Command.AcadimicYears
{
	public class CreateAcadimicYearCommand :ICommand <AcadimicYear>
	{
		public AcadimicYearDto AcadimicYearDto { get; set; }
	}
}
