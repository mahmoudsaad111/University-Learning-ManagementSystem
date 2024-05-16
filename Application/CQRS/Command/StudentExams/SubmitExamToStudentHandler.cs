using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.InterfacesForRepository;
using Application.Common.Interfaces.Presistance;
using Application.CQRS.Command.StudentExams;
using Contract.Dto.Exams;
using Contract.Dto.MCQs;
using Contract.Dto.TFQs;
using Domain.Enums;
using Domain.Models;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Command.StudentExams
{
    public class SubmitExamToStudentHandler : ICommandHandler<SubmitExamToStudentCommand, int>
    {
        private readonly IUnitOfwork unitOfwork;

        public SubmitExamToStudentHandler(IUnitOfwork unitOfwork)
        {
            this.unitOfwork = unitOfwork;
        }

        public async Task<Result<int>> Handle(SubmitExamToStudentCommand request, CancellationToken cancellationToken)
        {
            bool IsAdded = false;
            int addedone = 0;
            try
            {
                Exam ExamWorkNow = await unitOfwork.ExamRepository.GetExamIncludinigExamPlaceByExamId(request.studentAnswersOfExamDto.ExamId);
                if (ExamWorkNow == null)
                    return Result.Failure<int>(new Error(code: "Submit Studnet Asnwer to Exam", message: "Invalid ExamId"));

                if (DateTime.Now < ExamWorkNow.StratedAt || DateTime.Now > ExamWorkNow.StratedAt + ExamWorkNow.DeadLine)
                    return Result.Failure<int>(new Error(code: "Submit Studnet Asnwer to Exam", message: "can not get exam at this time"));

                ExamPlace examPlace = ExamWorkNow.ExamPlace;
                bool IfStudentHasAccessToExam = false;
                if (examPlace is null)
                    return Result.Failure<int>(new Error(code: "Submit Studnet Asnwer to Exam", message: "wrong examId"));

                if (examPlace.ExamType == ExamType.Quiz && examPlace.SectionId is not null)
                    IfStudentHasAccessToExam = await unitOfwork.StudentSectionRepository.CheckIfStudnetInSectionByUserName(
                        StudentUserName: request.studentAnswersOfExamDto.StudentUserName, SectionId: (int)examPlace.SectionId);

                else if ((examPlace.ExamType == ExamType.Quiz || examPlace.ExamType == ExamType.Midterm) && examPlace.CourseCycleId is not null)
                    IfStudentHasAccessToExam = await unitOfwork.StudentCourseCycleRepository.CheckIfStudnetInCourseCycle(
                        StudnetUserName: request.studentAnswersOfExamDto.StudentUserName, CourseCylceId: (int)examPlace.CourseCycleId);

                else if ((examPlace.ExamType == ExamType.Semester || examPlace.ExamType == ExamType.Final) && examPlace.CourseId is not null)
                    IfStudentHasAccessToExam = await unitOfwork.CourseRepository.CheckIfStudentHasAccessToCourse(
                        StudentUserName: request.studentAnswersOfExamDto.StudentUserName, CourseId: (int)examPlace.CourseId);


                if (!IfStudentHasAccessToExam)
                    return Result.Failure<int>(new Error(code: "Submit Studnet Asnwer to Exam", message: "Student has no access to exam"));


                var StudentId = await unitOfwork.StudentRepository.GetStudentIdUsingUserName(request.studentAnswersOfExamDto.StudentUserName);

                var AddStudentExam = await unitOfwork.StudentExamRepository.CreateAsync(new StudentExam
                {
                    StudentId = StudentId,
                    ExamId = request.studentAnswersOfExamDto.ExamId,
                    MarkOfStudentInExam = 0,
                    SubmitedAt = DateTime.Now
                });
                if (AddStudentExam is null)
                    return Result.Failure<int>(new Error(code: "Submit student answer in exam", message: "Can not add student to this exam "));

                await unitOfwork.SaveChangesAsync(); // make sure that StudentExam get Identitiy Id 

                List<MCQCorrectAnswer> mCQCorrectAnswers = (List<MCQCorrectAnswer>)await unitOfwork.MCQRepository.GetMCQOfExamWithCorrectAnswers(ExamId: request.studentAnswersOfExamDto.ExamId);

                var MCQTupleOfBoolAndInt = await unitOfwork.StudentExamRepository.AddStudentMCQAnswerOfExam(studentExamId: AddStudentExam.StudentExamId,
                    mCQCorrectAnswers = mCQCorrectAnswers,
                    MCQStudentAnswers: request.studentAnswersOfExamDto.StudentMCQAnswers);


                if (MCQTupleOfBoolAndInt is not null && MCQTupleOfBoolAndInt.Item1 == true)
                {
                    List<TFQCorrectAnswer> tFQCorrectAnswers = (List<TFQCorrectAnswer>)await unitOfwork.TFQRepository.GetMTFOfExamWithCorrectAnswer(ExamId: request.studentAnswersOfExamDto.ExamId);

                    var TFQTupleOfBoolAndInt = await unitOfwork.StudentExamRepository.AddStudentTFQAnswerOfExam(studentExamId: AddStudentExam.StudentExamId,
                        tFQCorrectAnswers = tFQCorrectAnswers,
                      TFQStudentAnswers: request.studentAnswersOfExamDto.StudentTFQAnswers);


                    if (TFQTupleOfBoolAndInt is not null && TFQTupleOfBoolAndInt.Item1 == true)
                    {
                        AddStudentExam.MarkOfStudentInExam = MCQTupleOfBoolAndInt.Item2 + TFQTupleOfBoolAndInt.Item2;
                        await unitOfwork.StudentExamRepository.UpdateAsync(AddStudentExam);
                        await unitOfwork.SaveChangesAsync();
                        return Result.Success<int>(MCQTupleOfBoolAndInt.Item2 + TFQTupleOfBoolAndInt.Item2);
                    }
                }

                bool Deleted = await unitOfwork.StudentExamRepository.DeleteAsync(AddStudentExam.StudentExamId);
                if (Deleted)
                    await unitOfwork.SaveChangesAsync();

                return Result.Failure<int>(new Error(code: "Submit student Answer in Exam", message: "Can not added student answers "));


            }
            catch (Exception ex)
            {
                if (IsAdded)
                {
                    await unitOfwork.StudentExamRepository.DeleteAsync(addedone);
                    await unitOfwork.SaveChangesAsync();
                }
                return Result.Failure<int>(new Error(code: "Submit student Answer in Exam", message: ex.Message.ToString()));
            }
        }

    }
}



//System.NullReferenceException
//  HResult = 0x80004003
//  Message=Object reference not set to an instance of an object.
//  Source=Infrastructure
//  StackTrace:
//   at Infrastructure.Repositories.StudentExamRepository.< AddStudentMCQAnswerOfExam > d__5.MoveNext() in C: \Users\lenovo\Desktop\NewCLone mahmoud saad\OnlineLearningModelSystemPlatform\Infrasructure\Repositories\StudentExamRepository.cs:line 44
//   at Application.CQRS.Command.StudentExams.SubmitExamToStudentHandler.<Handle>d__2.MoveNext() in C:\Users\lenovo\Desktop\NewCLone mahmoud saad\OnlineLearningModelSystemPlatform\Application\CQRS\Command\StudentExams\SubmitExamToStudentHandler.cs:line 76

//  This exception was originally thrown at this call stack:
//    Infrastructure.Repositories.StudentExamRepository.AddStudentMCQAnswerOfExam(int, System.Collections.Generic.List<Contract.Dto.QuestionsOfExam.MCQCorrectAnswer>, System.Collections.Generic.List<Contract.Dto.QuestionsOfExam.MCQStudentAnswerDto>) in StudentExamRepository.cs
//    Application.CQRS.Command.StudentExams.SubmitExamToStudentHandler.Handle(Application.CQRS.Command.StudentExams.SubmitExamToStudentCommand, System.Threading.CancellationToken) in SubmitExamToStudentHandler.cs