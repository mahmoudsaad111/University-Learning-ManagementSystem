using Application.Common.Interfaces.CQRSInterfaces;
using Contract.Dto.AssignementAnswers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Query.AssignementAnswers
{
    public class GetAllStudnetWithAnswersOnAssignementQuery : IQuery<IEnumerable<StudentHasAnswerOfAssignemet>>
    {
        public string ProfOrInstUserName {  get; set; }
        public int AssignemntId {  get; set; }  
        public bool IsInstructor { get; set; }  
    }
}
