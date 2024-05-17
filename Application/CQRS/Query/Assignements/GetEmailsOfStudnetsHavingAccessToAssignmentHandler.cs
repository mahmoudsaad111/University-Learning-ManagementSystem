using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Query.Assignements
{
    public class GetEmailsOfStudnetsHavingAccessToAssignmentHandler : IQueryHandler<GetEmailsOfStudnetsHavingAccessToAssignmentQuery, IEnumerable<string>>
    {
        private readonly IUnitOfwork unitOfwork;

        public GetEmailsOfStudnetsHavingAccessToAssignmentHandler(IUnitOfwork unitOfwork)
        {
            this.unitOfwork = unitOfwork;
        }

        public async Task<Result<IEnumerable<string>>> Handle(GetEmailsOfStudnetsHavingAccessToAssignmentQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var Assignment = await unitOfwork.AssignementRepository.GetByIdAsync(request.AssignmentId);
                if (Assignment is null)
                    return Result.Failure<IEnumerable<string>>(new Error(code: "GetEmailsOfStudnetsHavingAccessToAssignment", message: "Invalid data"));
                var EmailsOfStudents = await unitOfwork.AssignementRepository.GetEmailsOfStudnetsHavingAccessToAssignment(AssignmentId: request.AssignmentId);
                return Result.Success(EmailsOfStudents);
            }
            catch (Exception ex)
            {
                return Result.Failure<IEnumerable<string>>(new Error(code: "GetEmailsOfStudnetsHavingAccessToAssignment", message: ex.Message.ToString()));
            }
        }
    }
}
