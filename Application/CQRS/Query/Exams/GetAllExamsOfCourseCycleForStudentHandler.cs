using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Contract.Dto.Exams;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Query.Exams
{
    internal class GetAllExamsOfCourseCycleForStudentHandler : IQueryHandler<GetAllExamsOfCourseCycleForStudentQuery, IEnumerable<ExamOfStudentToDto>>
    {
        private readonly IUnitOfwork unitOfwork;

        public GetAllExamsOfCourseCycleForStudentHandler(IUnitOfwork unitOfwork)
        {
            this.unitOfwork = unitOfwork;
        }

        public async Task<Result<IEnumerable<ExamOfStudentToDto>>> Handle(GetAllExamsOfCourseCycleForStudentQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await unitOfwork.UserRepository.GetUserByUserName(request.StudentUserName);

                if (user is null)
                    return Result.Failure<IEnumerable<ExamOfStudentToDto>>(new Error(code: "GetAllExamsOfCourseCycleForStudent", message: "wrong data"));

                bool IfStudentInCourseCycle = await unitOfwork.StudentCourseCycleRepository.ChekcIfStudentInCourseCycle(StudentId: user.Id, CourseCycleId: request.CourseCycleId);

                if (!IfStudentInCourseCycle)
                    return Result.Failure<IEnumerable<ExamOfStudentToDto>>(new Error(code: "GetAllExamsOfCourseCycleForStudent", message: "user has no access"));

                var examsOfStudentOnCourseCycle = await unitOfwork.ExamRepository.GetAllExamsOfStudentInCourseCycle(courseCylceId: request.CourseCycleId, studentId: user.Id);

                return Result.Success<IEnumerable<ExamOfStudentToDto>>(examsOfStudentOnCourseCycle);    
 
            }
            catch (Exception ex)
            {
                return Result.Failure<IEnumerable<ExamOfStudentToDto>>(new Error(code: "GetAllExamsOfCourseCycleForStudent", message: ex.Message.ToString())) ;
            }
        }
    }
}
