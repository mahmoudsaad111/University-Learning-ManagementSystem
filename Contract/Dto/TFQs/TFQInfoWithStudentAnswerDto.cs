using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Dto.TFQs
{
    public class TFQInfoWithStudentAnswerDto
    {
        public string Text {  get; set; }   
        public bool CorectAnswer { get; set; }    
        public bool StudetAnswer { get; set; }
        public int Degree { get; set; }

    }
}
