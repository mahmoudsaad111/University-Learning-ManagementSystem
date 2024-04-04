using Application.Common.Interfaces.CQRSInterfaces;
using Domain.Models;
 

namespace Application.CQRS.Query.AcadimicYears
{
    public class GetAllAcadimicYearsQuery : IQuery<IEnumerable<AcadimicYear>>
    {
    }
}
