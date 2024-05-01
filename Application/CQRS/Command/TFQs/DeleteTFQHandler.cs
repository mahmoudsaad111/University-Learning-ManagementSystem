using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Command.TFQs
{
    public class DeleteTFQHandler : ICommandHandler<DeleteTFQCommand, int>
    {
        private readonly IUnitOfwork unitOfwork;

        public DeleteTFQHandler(IUnitOfwork unitOfwork)
        {
            this.unitOfwork = unitOfwork;
        }

        public async Task<Result<int>> Handle(DeleteTFQCommand request, CancellationToken cancellationToken)
        {
            try
            {
               var TFQ = await unitOfwork.TFQRepository.GetByIdAsync(request.TFQId);
                if (TFQ is not null)
                {
                    bool Deleted = await unitOfwork.TFQRepository.DeleteAsync(request.TFQId);
                    if (Deleted)
                    {
                        bool UpdateFullMarkOfExam = await unitOfwork.ExamRepository.UpdateExamFullMarksAccordingToCommandOfQuestion(TFQ.ExamId, NewQuestionDegree: 0, OldQuestionDegree: TFQ.Degree);
                        if (UpdateFullMarkOfExam)
                        {
                            await unitOfwork.SaveChangesAsync();
                            return Result.Success<int>(request.TFQId);
                        }
                    }
                    return Result.Failure<int>(new Error(code: "Delete TFQ", message: "Can not delete the Mc question"));
                }
                return Result.Failure<int>(new Error(code: "Delete TFQ", message: "wrong Id"));

            }
            catch (Exception ex)
            {
                return Result.Failure<int>(new Error(code: "Delete TFQ", message: ex.Message.ToString())) ;
            }
        }
    }
}
