using Application.Common.Interfaces.CQRSInterfaces;
using Contract.Dto.StudentCourseCycles;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Command.StudentCourseCycles
{
    public class CreateStudentCourseCycleCommand :ICommand<StudentCourseCycle>
    {
        public StudentCourseCycleDto StudentCourseCycleDto { get; set; }
    }
}
