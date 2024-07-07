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
    public class GetAllLecturesForStudentQuery : IQuery<IEnumerable<Lecture>>
    {
        public GetLectureForStudentDto getLectureForStudentDto { get; set; }
    }
}
