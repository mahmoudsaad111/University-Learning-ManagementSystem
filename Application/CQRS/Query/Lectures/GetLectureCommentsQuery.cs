using Application.Common.Interfaces.CQRSInterfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Query.Lectures
{
    public class GetLectureCommentsQuery :IQuery<IEnumerable<Comment>>
    {
        public int LectureId { get; set; } 
    }
}
