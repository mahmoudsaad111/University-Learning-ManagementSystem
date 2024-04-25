using Application.Common.Interfaces.CQRSInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Application.CQRS.Command.StudentCourseCycles
{
    public class DeleteStudentCourseCycleCommand : ICommand<int>
    {
        public int Id { get; set; } 
    }
}
