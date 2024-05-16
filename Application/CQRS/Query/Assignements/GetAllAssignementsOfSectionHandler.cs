using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Domain.Models;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Query.Assignements
{
    public class GetAllAssignementsOfSectionHandler : IQueryHandler<GetAllAssignementsOfSectionQuery, IEnumerable<Assignment>>
    {
        private readonly IUnitOfwork unitOfwork;
        public GetAllAssignementsOfSectionHandler(IUnitOfwork unitOfwork)
        {
            this.unitOfwork = unitOfwork;
        }

        public async Task<Result<IEnumerable<Assignment>>> Handle(GetAllAssignementsOfSectionQuery request, CancellationToken cancellationToken)
        {
            try
            {
                bool IfUserHasAccessToSection = false;  
                var User = await unitOfwork.UserRepository.GetUserByUserName(request.ProfOrInstUserName);

                if (User is null)
                    return Result.Failure<IEnumerable<Assignment>>(new Error(code: "GetAllAssignementsOfSectionQuery", message: "Invalid data"));

                if (request.IsInstructor)
                    IfUserHasAccessToSection = await unitOfwork.SectionRepository.CheckIfInstructorInSection(SectionId: request.SectionId, InstrucotrId: User.Id);
                else
                    IfUserHasAccessToSection = await unitOfwork.SectionRepository.CheckIfProfessorInSection(SectionId: request.SectionId, ProfessorId: User.Id);

                if(! IfUserHasAccessToSection)
                    return Result.Failure<IEnumerable<Assignment>>(new Error(code: "GetAllAssignementsOfSectionQuery", message: "Has no access"));

                var AssignemntsOfSection = await unitOfwork.AssignementRepository.GetAllAssignementsOfSection(sectionId : request.SectionId);   

                return Result.Success(AssignemntsOfSection);
            }
            catch (Exception ex)
            {
                return Result.Failure<IEnumerable<Assignment>>(new Error(code: "GetAllAssignementsOfSectionQuery", message: ex.Message.ToString()));
            }
        }
    }
}
