using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Dto.AcadimicYears
{
    public class AcadimicYearDto
    {
        public int Year { get; set; }
        public int DepartementId { get; set; }

        public AcadimicYear GetAcadimicYear()
        {
            return new AcadimicYear
            {
                Year = Year,
                DepartementId = DepartementId
            };
        }
    }
}
