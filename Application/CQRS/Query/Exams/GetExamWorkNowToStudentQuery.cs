using Application.Common.Interfaces.CQRSInterfaces;
using Contract.Dto.Exams;
 
namespace Application.CQRS.Query.Exams
{
    public class GetExamWorkNowToStudentQuery :IQuery<ExamWrokNowDto>
    {
        public string StudentUserName { get; set; }    
        public int ExamId {  get; set; }    
    }
}
