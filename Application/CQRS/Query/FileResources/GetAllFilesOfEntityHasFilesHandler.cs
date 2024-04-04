using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Query.FileResources
{
    public class GetAllFilesOfEntityHasFilesHandler : IQueryHandler<GetAllFilesOfEntityHasFilesQuery, IEnumerable<string>>
    {
        private readonly IUnitOfwork unitOfwork;

        public GetAllFilesOfEntityHasFilesHandler(IUnitOfwork unitOfwork)
        {
            this.unitOfwork = unitOfwork;
        }

        public async Task<Result<IEnumerable<string>>> Handle(GetAllFilesOfEntityHasFilesQuery request, CancellationToken cancellationToken)
        {
            try
            {         
                IEnumerable<string> result=new List<string>();
                if (request.TypeOfEntity == enums.EntitiesHasFiles.Lecture)
                    result = await unitOfwork.LectureResourceRepository.GetAllFilesUrlForLectureAsync(request.LectureId);
                else if (request.TypeOfEntity == enums.EntitiesHasFiles.Assignement)
                    result = await unitOfwork.AssignementResourceRepository.GetAllFilesUrlForAssignementAsync(request.AssignmentId);
                else if(request.TypeOfEntity==enums.EntitiesHasFiles.AssignementAnswer)
                    result=await unitOfwork.AssignementAnswerResouceRepository.GetAllFilesUrlForAssignementAnswerAsync(request.AssignmentAnswerId);
                    
                return Result.Success<IEnumerable<string>>(result); 
            }
            catch(Exception ex)
            {
                return Result.Failure<IEnumerable<string>>(new Error("", ""));
            }
        }
    }
}
