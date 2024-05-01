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

namespace Application.CQRS.Command.TFQs
{
    public class CreateTFQHandlercs : ICommandHandler<CreateTFQCommand, TrueFalseQuestion>
    {
        private readonly IUnitOfwork unitOfwork;

        public CreateTFQHandlercs(IUnitOfwork unitOfwork)
        {
            this.unitOfwork = unitOfwork;
        }

        public async Task<Result<TrueFalseQuestion>> Handle(CreateTFQCommand request, CancellationToken cancellationToken)
        {
            try
            {
                TrueFalseQuestion TFQ = await unitOfwork.TFQRepository.CreateAsync(request.TFQDto.GetTFQ());

                if(TFQ is not null)
                {
                    bool UpdateFullMarkOfExam = await unitOfwork.ExamRepository.UpdateExamFullMarksAccordingToCommandOfQuestion(TFQ.ExamId, NewQuestionDegree: request.TFQDto.Degree, OldQuestionDegree: 0);
                    if (UpdateFullMarkOfExam)
                    {
                        await unitOfwork.SaveChangesAsync();
                        return Result.Success<TrueFalseQuestion>(TFQ);
                    }
                }
                return Result.Failure<TrueFalseQuestion>(new Error(code: "Create TFQ", message: "Can not create new question")) ;

            }
            catch (Exception ex)
            {
                return Result.Failure<TrueFalseQuestion>(new Error(code : "Create TFQ" , message : ex.Message.ToString())); 
            }
        }
    }
}
