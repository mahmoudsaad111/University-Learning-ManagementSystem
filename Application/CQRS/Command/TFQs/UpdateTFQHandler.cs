using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Domain.Enums;
using Domain.Models;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Command.TFQs
{
    public class UpdateTFQHandler : ICommandHandler<UpdateTFQCommand, TrueFalseQuestion>
    {
        private readonly IUnitOfwork unitOfwork;

        public UpdateTFQHandler(IUnitOfwork unitOfwork)
        {
            this.unitOfwork = unitOfwork;
        }

        public async Task<Result<TrueFalseQuestion>> Handle(UpdateTFQCommand request, CancellationToken cancellationToken)
        {
            try
            {
                TrueFalseQuestion OldTFQ = await unitOfwork.TFQRepository.GetByIdAsync(request.TFQId);
                if (OldTFQ == null)
                    return Result.Failure<TrueFalseQuestion>(new Error(code: "Update TFQ", message: "No Question by this Id"));
                if (OldTFQ.ExamId != request.TFQDto.ExamId)
                    return Result.Failure<TrueFalseQuestion>(new Error(code: "Update TFQ", message: "Can not change the question to be in other exam"));

                int OldQuestionDegree = OldTFQ.Degree;
                 OldTFQ.Text=request.TFQDto.Text;
                 OldTFQ.IsTrue=request.TFQDto.IsTrue;
                 OldTFQ.Degree=request.TFQDto.Degree;


                bool Updated = await unitOfwork.TFQRepository.UpdateAsync(OldTFQ);

                if (Updated)
                {
                    bool UpdateFullMarkOfExam = true;
                    if (OldQuestionDegree != request.TFQDto.Degree)
                    {
                        UpdateFullMarkOfExam = await unitOfwork.ExamRepository.UpdateExamFullMarksAccordingToCommandOfQuestion(OldTFQ.ExamId, NewQuestionDegree: request.TFQDto.Degree, OldQuestionDegree: OldQuestionDegree);
                    }
                    if (UpdateFullMarkOfExam)
                    {
                        await unitOfwork.SaveChangesAsync();
                        return Result.Success<TrueFalseQuestion>(OldTFQ);
                    }
                }
                return Result.Failure<TrueFalseQuestion>(new Error(code: "Update TFQ", message: "Can not update the TFQ")) ;

            }
            catch (Exception ex)
            {
                return Result.Failure<TrueFalseQuestion>(new Error(code: "Update TFQ", message: ex.Message.ToString()));
            }
        }
    }
}
