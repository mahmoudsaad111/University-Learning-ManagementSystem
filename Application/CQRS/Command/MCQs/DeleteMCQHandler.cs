using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Command.MCQs
{
    public class DeleteMCQHandler : ICommandHandler<DeleteMCQCommand, int>
    {
        private readonly IUnitOfwork unitOfwork;

        public DeleteMCQHandler(IUnitOfwork unitOfwork)
        {
            this.unitOfwork = unitOfwork;
        }

        public async Task<Result<int>> Handle(DeleteMCQCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var MCQ = await unitOfwork.MCQRepository.GetByIdAsync(request.MCQId);
                if (MCQ is not null)
                {
                    bool Deleted = await unitOfwork.MCQRepository.DeleteAsync(request.MCQId);
                    if (Deleted)
                    {
                        bool UpdateFullMarkOfExam = await unitOfwork.ExamRepository.UpdateExamFullMarksAccordingToCommandOfQuestion(MCQ.ExamId, NewQuestionDegree: 0, OldQuestionDegree: MCQ.Degree) ;
                        if (UpdateFullMarkOfExam)
                        {
                            await unitOfwork.SaveChangesAsync();
                            return Result.Success<int>(request.MCQId);
                        }
                    }
                    return Result.Failure<int>(new Error(code: "Delete Mcq", message: "Can not delete the Mc question"));
                }
                return Result.Failure<int>(new Error(code: "Delete Mcq", message: "wrong Id"));
            }
            catch (Exception ex)
            {
                return Result.Failure<int>(new Error(code: "Delete Mcq", message: ex.Message.ToString())) ;
            }
        }
    }
}
