using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Domain.Shared;

namespace Application.CQRS.Command.Exams
{
    public class DeleteExamHandler : ICommandHandler<DeleteExamCommand, int>
    {
        private readonly IUnitOfwork unitOfwork;

        public DeleteExamHandler(IUnitOfwork unitOfwork)
        {
            this.unitOfwork = unitOfwork;
        }

        public async Task<Result<int>> Handle( DeleteExamCommand request, CancellationToken cancellationToken)
        {
            try
            {
                bool Delted = await unitOfwork.ExamRepository.DeleteAsync(request.ExamId);
                if (Delted)
                {
                    await unitOfwork.SaveChangesAsync();
                    return Result.Success<int>(request.ExamId);
                }

                return Result.Failure<int>(new Error(code: "DeleteExam", message: "No Exam has this Id")) ;
            }
            catch (Exception ex)
            {
                return Result.Failure<int>(new Error(code: "DeleteExam", message: ex.Message.ToString()) );
            }
        }
    }
}
