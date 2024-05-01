using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Domain.Enums;
using Domain.Models;
using Domain.Shared;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Command.MCQs
{
    public class UpdateMCQHandler : ICommandHandler<UpdateMCQCommand, MultipleChoiceQuestion>
    {
        private readonly IUnitOfwork unitOfwork;

        public UpdateMCQHandler(IUnitOfwork unitOfwork)
        {
            this.unitOfwork = unitOfwork;
        }

        public async Task<Result<MultipleChoiceQuestion>> Handle(UpdateMCQCommand request, CancellationToken cancellationToken)
        {
            try
            {
                MultipleChoiceQuestion OldMcq = await unitOfwork.MCQRepository.GetByIdAsync(request.MCQId);
                if (OldMcq == null)
                    return Result.Failure<MultipleChoiceQuestion>(new Error(code: "Update Mcq", message: "No Question by this Id"));
                if (OldMcq.ExamId != request.MCQDto.ExamId)
                    return Result.Failure<MultipleChoiceQuestion>(new Error(code: "Update Mcq", message: "Can not change the question to be in other exam"));

                if ( request.MCQDto.CorrectAnswer != MCQOptions.OptionA && request.MCQDto.CorrectAnswer != MCQOptions.OptionB &&
                     request.MCQDto.CorrectAnswer != MCQOptions.OptionC && request.MCQDto.CorrectAnswer != MCQOptions.OptionD
                   )
                    return Result.Failure<MultipleChoiceQuestion>(new Error(code: "Update MCQ", message: "Correct answer in not on the four options answers"));

                int OldQuestionDegree = OldMcq.Degree; 

                 OldMcq.Text=request.MCQDto.Text;
                 OldMcq.OptionA = request.MCQDto.OptionA;
                 OldMcq.OptionB= request.MCQDto.OptionB;
                 OldMcq.OptionC = request.MCQDto.OptionC;
                 OldMcq.OptionD= request.MCQDto.OptionD;    
                 OldMcq.CorrectAnswer= request.MCQDto.CorrectAnswer;
                 OldMcq.Degree=request.MCQDto.Degree;
                bool Updated = await unitOfwork.MCQRepository.UpdateAsync(OldMcq);

                if (Updated)
                {
                    bool UpdateFullMarkOfExam = true; 
                    if (OldQuestionDegree != request.MCQDto.Degree)
                    {
                        UpdateFullMarkOfExam = await unitOfwork.ExamRepository.UpdateExamFullMarksAccordingToCommandOfQuestion(OldMcq.ExamId,
                                                                                                                               NewQuestionDegree: request.MCQDto.Degree,
                                                                                                                               OldQuestionDegree: OldQuestionDegree);
                    }
                    if (UpdateFullMarkOfExam)
                    {
                        await unitOfwork.SaveChangesAsync();
                        return Result.Success<MultipleChoiceQuestion>(OldMcq);
                    }
                }
                return Result.Failure<MultipleChoiceQuestion>(new Error(code: "Update MCQ", message: "Can not update the MCQ")) ;

            }
            catch (Exception ex)
            {
                return Result.Failure<MultipleChoiceQuestion>(new Error(code: "Update Mcq", message: ex.Message.ToString()));
            }
        }
    }
}
