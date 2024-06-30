using Application.Common.Interfaces.CQRSInterfaces;
using Contract.Dto.Groups;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Query.Groups
{
    public class GetGroupChatQuery : IQuery<IEnumerable<Message>>
    {
        public int GroupId { get; set;  } 
    }
}
