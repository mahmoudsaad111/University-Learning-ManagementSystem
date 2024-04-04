using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Application.CQRS.Command.FileResources;
using Domain.Models;
using Domain.Shared;
 

namespace Application.CQRS.Command.LectureResourses
{
    public class CreateFileResourceHandler : ICommandHandler<CreateFileResourceCommand, FileResource>
    {
        private readonly IUnitOfwork unitOfwork;

        public CreateFileResourceHandler(IUnitOfwork unitOfwork)
        {
            this.unitOfwork = unitOfwork;
        }
        public async Task<Result<FileResource>> Handle(CreateFileResourceCommand request, CancellationToken cancellationToken)
        {
            string TypeOfEntity = string.Empty;
            try
            {
                FileResource TypeOfEntityHasFile = null;             

                if (request.TypeOfEntity == enums.EntitiesHasFiles.Lecture)
                {
                    TypeOfEntityHasFile = request.FileResourceDto.GetLectureResource();
                    TypeOfEntity = "Lecture";
                }
                else if (request.TypeOfEntity == enums.EntitiesHasFiles.Assignement)
                {
                    TypeOfEntityHasFile = request.FileResourceDto.GetAssignmentResource();
                    TypeOfEntity = "Assignment";
                }
                else if (request.TypeOfEntity == enums.EntitiesHasFiles.AssignementAnswer)
                {
                    TypeOfEntityHasFile = request.FileResourceDto.GetAssignmentAnswerResource();
                    TypeOfEntity = "AssignmentAnswer";
                }

                var createdResource =   await unitOfwork.FileResourceRepository.CreateAsync(TypeOfEntityHasFile);
                if (createdResource is null)
                    return Result.Failure<FileResource>(new Error(code: $"Added {TypeOfEntity}Resource", message:$"Unable to add {TypeOfEntity}Resource"));

                await unitOfwork.SaveChangesAsync();
                return Result.Success<FileResource>(createdResource);
            }
            catch (Exception ex)
            {
                return Result.Failure<FileResource>(new Error($"can't create new {TypeOfEntity}Resource", ex.Message.ToString()));
            }
        }
    }
}
 