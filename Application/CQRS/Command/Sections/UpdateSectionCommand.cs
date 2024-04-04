using Application.Common.Interfaces.CQRSInterfaces;
using Contract.Dto.Sections;
 
using Domain.Models;
 

namespace Application.CQRS.Command.Sections
{
	public class UpdateSectionCommand  : ICommand<Section>
	{
		public int Id { get; set; }
		public SectionDto SectionDto { get; set; }
	}
}
