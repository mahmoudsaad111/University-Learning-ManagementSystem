using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Contract.Dto.Exams;
using Contract.Dto.StudentExamDto;
using Domain.Enums;
using Domain.Models;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Query.Exams
{
    public class GetAllStudentsAttendExamHandler : IQueryHandler<GetAllStudentsAttendExamQuery, IEnumerable<StudentAttendExamDto>>
    {
        private readonly IUnitOfwork unitOfwork;

        public GetAllStudentsAttendExamHandler(IUnitOfwork unitOfwork)
        {
            this.unitOfwork = unitOfwork;
        }

        public async Task<Result<IEnumerable<StudentAttendExamDto>>> Handle(GetAllStudentsAttendExamQuery request, CancellationToken cancellationToken)
        {
            try
            {
                Exam examWillDeleted = await unitOfwork.ExamRepository.GetByIdAsync(request.ExamId);
                if (examWillDeleted is null)
                    return Result.Failure<IEnumerable<StudentAttendExamDto>>(new Error(code: "GetAllStudentsAttendExam", message: "No Exam has this Id"));

                bool IfUserHasAccessToExam = false;

                ExamPlace examPlaceOfCurrentExam = await unitOfwork.ExamPlaceRepository.GetByIdAsync(examWillDeleted.ExamPlaceId);
                var user = await unitOfwork.UserRepository.GetUserByUserName(request.ExamCreatorUserName);

                if (user is null)
                    return Result.Failure<IEnumerable<StudentAttendExamDto>>(new Error(code: "GetAllStudentsAttendExam", message: "user has no access"));


                if (examPlaceOfCurrentExam is null)
                    return Result.Failure<IEnumerable<StudentAttendExamDto>>(new Error(code: "GetAllStudentsAttendExam", message: "can not load questions"));

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
                    return Result.Failure<IEnumerable<StudentAttendExamDto>>(new Error(code: "GetAllStudentsAttendExam", message: "user has no access"));

                var StudentsAttendExam = await unitOfwork.ExamRepository.GetAllStudentsAttendExam(ExamId: request.ExamId);

                return Result.Success(StudentsAttendExam);
            }
            catch (Exception ex)
            {
                    return Result.Failure<IEnumerable<StudentAttendExamDto>>(new Error(code: "GetAllStudentsAttendExam", message: ex.Message.ToString()));

            }
        }
    }
}
