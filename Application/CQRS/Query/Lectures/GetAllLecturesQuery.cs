using Application.Common.Interfaces.CQRSInterfaces;
using Contract.Dto.Lectures;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Query.Lectures
{
    public class GetAllLecturesQuery :IQuery<IEnumerable<Lecture>>
    {
       public GetLectureDto GetLectureDto { get; set; }   
    }
}
