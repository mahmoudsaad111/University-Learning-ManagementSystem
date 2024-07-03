using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Domain.Models;
using Domain.Shared;
using Domain.TmpFilesProcessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Query.FileResources
{
    public class GetAllFilesOfEntityHasFilesHandler : IQueryHandler<GetAllFilesOfEntityHasFilesQuery, IEnumerable<IFileResourceModel>>
    {
        private readonly IUnitOfwork unitOfwork;

        public GetAllFilesOfEntityHasFilesHandler(IUnitOfwork unitOfwork)
        {
            this.unitOfwork = unitOfwork;
        }

        public async Task<Result<IEnumerable<IFileResourceModel>>> Handle(GetAllFilesOfEntityHasFilesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                IEnumerable<IFileResourceModel> result = new List<IFileResourceModel>();
                if (request.TypeOfEntity == enums.EntitiesHasFiles.Lecture)
                    result = await unitOfwork.LectureResourceRepository.GetAllFilesUrlForLectureAsync(request.LectureId);
                else if (request.TypeOfEntity == enums.EntitiesHasFiles.Assignement)
                    result = await  unitOfwork.AssignementResourceRepository.GetAllFilesUrlForAssignementAsync(request.AssignmentId);
                else if(request.TypeOfEntity==enums.EntitiesHasFiles.AssignementAnswer)
                   result=await unitOfwork.AssignementAnswerResourceRepository.GetAllFilesUrlForAssignementAnswerAsync(request.AssignmentAnswerId);
                    
                return Result.Success<IEnumerable<IFileResourceModel>>(result); 
            }
            catch(Exception ex)
            {
                return Result.Failure<IEnumerable<IFileResourceModel>>(new Error("GetAllFilesOfEntityHasFiles", ex.Message.ToString()));
            }
        }
    }
}
