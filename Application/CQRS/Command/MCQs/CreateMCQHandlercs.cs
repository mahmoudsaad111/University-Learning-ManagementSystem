using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Domain.Models;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums; 

namespace Application.CQRS.Command.MCQs
{
    public class CreateMCQHandlercs : ICommandHandler<CreateMCQCommand, MultipleChoiceQuestion>
    {
        private readonly IUnitOfwork unitOfwork;

        public CreateMCQHandlercs(IUnitOfwork unitOfwork)
        {
            this.unitOfwork = unitOfwork;
        }

        public async Task<Result<MultipleChoiceQuestion>> Handle(CreateMCQCommand request, CancellationToken cancellationToken)
        {
            try
            {

                if (request.MCQDto.CorrectAnswer != MCQOptions.OptionA && request.MCQDto.CorrectAnswer != MCQOptions.OptionB &&
                    request.MCQDto.CorrectAnswer != MCQOptions.OptionC && request.MCQDto.CorrectAnswer != MCQOptions.OptionD)

                    return Result.Failure<MultipleChoiceQuestion>(new Error(code: "Create MCQ", message: "Correct answer in not on the four options answers"));

                MultipleChoiceQuestion MCQ = await unitOfwork.MCQRepository.CreateAsync(request.MCQDto.GetMCQ());

                if(MCQ is not null)
                {

                    bool UpdateFullMarkOfExam = await unitOfwork.ExamRepository.UpdateExamFullMarksAccordingToCommandOfQuestion(MCQ.ExamId, NewQuestionDegree: request.MCQDto.Degree, OldQuestionDegree: 0);
                    if (UpdateFullMarkOfExam)
                    {
                        await unitOfwork.SaveChangesAsync();
                        return Result.Success<MultipleChoiceQuestion>(MCQ);
                    }
                    return Result.Failure<MultipleChoiceQuestion>(new Error(code: "Create Mcq", message: "Can not create new question"));

                }
                return Result.Failure<MultipleChoiceQuestion>(new Error(code: "Create Mcq", message: "Can not create new question")) ;

            }
            catch (Exception ex)
            {
                return Result.Failure<MultipleChoiceQuestion>(new Error(code : "Create Mcq" , message : ex.Message.ToString())); 
            }
        }
    }
}
