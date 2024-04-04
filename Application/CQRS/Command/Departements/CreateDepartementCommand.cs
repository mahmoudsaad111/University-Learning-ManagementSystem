using Application.Common.Interfaces.CQRSInterfaces;
using Contract.Dto.Departements;
using Domain.Models;

namespace Application.CQRS.Command.Departements
{
    public class CreateDepartementCommand : ICommand<Departement>
	{
		public DepartementDto DepartementDto { get; set; }
	}
}
