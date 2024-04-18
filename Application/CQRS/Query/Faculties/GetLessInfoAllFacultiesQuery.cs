using Application.Common.Interfaces.CQRSInterfaces;
using Contract.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Query.Faculties
{
    public class GetLessInfoAllFacultiesQuery : IQuery<IEnumerable<NameIdDto>>
    {
    }
}
