using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Dto.Groups
{
    public class GroupLessInfoDto
    {
        public int GroupId { get; set; }
        public string Name { get; set; }
        public string StudentHeadName { get; set; } = null!;
        public string StudentHeadPhone { get; set; } = null!;
        public short NumberOfStudent { get; set; }
        public int Year { get; set; }
        public int AcadimicYearId { get; set; }
    }
}
