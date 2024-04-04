using Application.Common.Interfaces.CQRSInterfaces;
using Contract.Dto.AcadimicYears;
 
using Domain.Models;
 
namespace Application.CQRS.Command.AcadimicYears
{
	public class UpdateAcadimicYearCommand  : ICommand<AcadimicYear>
	{
		public int Id { get; set; }
		public AcadimicYearDto AcadimicYearDto { get; set; }
	}
}
