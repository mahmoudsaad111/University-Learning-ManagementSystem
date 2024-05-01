using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Domain.Models;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Command.Exams
{
    public class UpdateExamHandler : ICommandHandler<UpdateExamCommand, Exam>
    {
        private readonly IUnitOfwork unitOfwork;

        public UpdateExamHandler(IUnitOfwork unitOfwork)
        {
            this.unitOfwork = unitOfwork;
        }

        public async Task<Result<Exam>> Handle(UpdateExamCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Exam OldExam = await unitOfwork.ExamRepository.GetByIdAsync(request.ExamId);
                if (OldExam == null)
                    return Result.Failure<Exam>(new Error(code: "UpdateExam", message: "No Exam Exist")) ;

                if (request.UpdateExamDto.SartedAt < DateTime.UtcNow)
                    return Result.Failure<Exam>(new Error(code: "Create Exam", message: "Can not create exam with this data time"));

             
                OldExam.Name=request.UpdateExamDto.Name;
                OldExam.FullMark = request.UpdateExamDto.FaullMarks;
                OldExam.StratedAt=request.UpdateExamDto.SartedAt;
                OldExam.DeadLine=request.UpdateExamDto.DeadLine;


                bool Updated = await unitOfwork.ExamRepository.UpdateAsync(OldExam);
                if (Updated)
                {
                    await unitOfwork.SaveChangesAsync();
                    return Result.Success<Exam>(OldExam);
                }
                return Result.Failure<Exam>(new Error(code: "Update Exam", message: "Can not updated exam with these data"));
            }
            catch (Exception ex)
            {
                return Result.Failure<Exam>(new Error(code: "Update Exam", message: ex.Message.ToString())) ;

            }
        }
    }
}
