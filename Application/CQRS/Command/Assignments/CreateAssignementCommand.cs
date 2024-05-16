using Application.Common.Interfaces.CQRSInterfaces;
using Contract.Dto.Assignements;
using Domain.Models;


namespace Application.CQRS.Command.Assignements
{
    public class CreateAssignementCommand : ICommand<Assignment>
    {
        public AssignementDto AssignementDto { get; set; }
        public string InstructorUserName { get; set; }
    }
}
