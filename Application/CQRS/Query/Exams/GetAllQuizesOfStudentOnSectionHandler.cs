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
    public class GetAllQuizesOfStudentOnSectionHandler : IQueryHandler<GetAllQuizessOfStudentOnSectionQuery, IEnumerable<ExamOfStudentToDto>>
    {
        private readonly IUnitOfwork unitOfwork;

        public GetAllQuizesOfStudentOnSectionHandler(IUnitOfwork unitOfwork)
        {
            this.unitOfwork = unitOfwork;
        }

        public async Task<Result<IEnumerable<ExamOfStudentToDto>>> Handle(GetAllQuizessOfStudentOnSectionQuery request, CancellationToken cancellationToken)
        {
            try
            {
                // check if user has access to this section or not
                var user = await unitOfwork.UserRepository.GetUserByUserName(request.StudentUserName);
                if (user is null)
                    return Result.Failure<IEnumerable<ExamOfStudentToDto>>(new Error(code: "GetAllQuizesOfStudentOnSection", message: "has no access"));

                bool CheckIfstudentSection = await unitOfwork.StudentSectionRepository.CheckIfStudentInSection(StudentId: user.Id, SectionId: request.SectionId); 
                if(!CheckIfstudentSection)
                    return Result.Failure<IEnumerable<ExamOfStudentToDto>>(new Error(code: "GetAllQuizesOfStudentOnSection", message: "has no access"));
                
                var examsOfStudentInSection = await unitOfwork.ExamRepository.GetAllExamsOfStudentInSection(sectionId:request.SectionId , studentId: user.Id);

                return Result.Success<IEnumerable<ExamOfStudentToDto>>(examsOfStudentInSection);

            }
            catch (Exception ex)
            {
                return Result.Failure<IEnumerable<ExamOfStudentToDto>>(new Error(code: "GetAllQuizesOfStudentOnSection", message: ex.Message.ToString()));
            }
        }
    }
}

