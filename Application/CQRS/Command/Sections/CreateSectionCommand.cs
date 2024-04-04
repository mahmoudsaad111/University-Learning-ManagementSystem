using Application.Common.Interfaces.CQRSInterfaces;
using Contract.Dto.Sections;
using Domain.Models;


namespace Application.CQRS.Command.Sections
{
	public class CreateSectionCommand : ICommand<Section>
	{
		public SectionDto SectionDto { get; set; }
	}
}
