using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Contract.Dto.Exams;
using Domain.Enums;
using Domain.Models;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Application.CQRS.Query.Exams
{
    public class GetAllQuestionsOfExamHandler : IQueryHandler<GetAllQuestionsOfExamQuery, QuestionsOfExamDto>
    {
        private readonly IUnitOfwork unitOfwork;

        public GetAllQuestionsOfExamHandler(IUnitOfwork unitOfwork)
        {
            this.unitOfwork = unitOfwork;
        }

        public async Task<Result<QuestionsOfExamDto>> Handle(GetAllQuestionsOfExamQuery request, CancellationToken cancellationToken)
        {
            try
            {
                Exam examWillDeleted = await unitOfwork.ExamRepository.GetByIdAsync(request.ExamId);
                if (examWillDeleted is null)
                    return Result.Failure<QuestionsOfExamDto>(new Error(code: "GetAllQuestionsOfExamQuery", message: "No Exam has this Id"));

                bool IfUserHasAccessToExam = false;

                ExamPlace examPlaceOfCurrentExam = await unitOfwork.ExamPlaceRepository.GetByIdAsync(examWillDeleted.ExamPlaceId);
                var user = await unitOfwork.UserRepository.GetUserByUserName(request.ExamCreatorUserName);

                if (user is null)
                    return Result.Failure<QuestionsOfExamDto>(new Error(code: "GetAllQuestionsOfExamQuery", message: "user has no access"));


                if (examPlaceOfCurrentExam is null)
                    return Result.Failure<QuestionsOfExamDto>(new Error(code: "GetAllQuestionsOfExamQuery", message: "can not load questions"));

                if (examPlaceOfCurrentExam.ExamType == ExamType.Quiz && examPlaceOfCurrentExam.SectionId is not null)
                {
                    IfUserHasAccessToExam = await unitOfwork.SectionRepository.CheckIfInstructorInSection(InstrucotrId: user.Id, SectionId: (int)examPlaceOfCurrentExam.SectionId);
                }
                else if ((examPlaceOfCurrentExam.ExamType == ExamType.Quiz || examPlaceOfCurrentExam.ExamType == ExamType.Midterm) && examPlaceOfCurrentExam.CourseCycleId is not null)
                {
                    IfUserHasAccessToExam = await unitOfwork.CourseCycleRepository.CheckIfProfInCourseCycle(ProfId: user.Id, CourseCycleId: (int)examPlaceOfCurrentExam.CourseCycleId);
                }
                else if (examPlaceOfCurrentExam.ExamType == ExamType.Final || examPlaceOfCurrentExam.ExamType == ExamType.Semester)
                {
                    IfUserHasAccessToExam = true;
                }

                if (!IfUserHasAccessToExam)
                    return Result.Failure<QuestionsOfExamDto>(new Error(code: "GetAllQuestionsOfExamQuery", message: "user has no access"));

                QuestionsOfExamDto questionsOfExamDto = new QuestionsOfExamDto
                {
                    TFQs = await unitOfwork.TFQRepository.GetAllTFQDetailsOfExams(request.ExamId),
                    MCQs = await unitOfwork.MCQRepository.GetAllMCQDetailsOfExams(request.ExamId)
                };

                return Result.Success<QuestionsOfExamDto>(questionsOfExamDto);
            }
            catch (Exception ex)
            {
                return Result.Failure<QuestionsOfExamDto>(new Error(code: "GetAllQuestionsOfExamQuery", message: ex.Message.ToString()));
            }
        }
    }
}
