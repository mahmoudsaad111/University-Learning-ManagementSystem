using Application.Common.Interfaces.CQRSInterfaces;
using Contract.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Query.Departements
{
    public class GetDepartementBelongsToFacultyQuery : IQuery<IEnumerable<NameIdDto>>
    {
        public int FacultyId {  get; set; }
    }
}
