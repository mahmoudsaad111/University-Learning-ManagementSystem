using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.FileProcessing;
using Application.Common.Interfaces.Presistance;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Command.FilesProcessing
{
    public class UploadFileToAssignementAnswerHandler : ICommandHandler<UploadFileToAssignementAnswerCommand, string>
    {
        private readonly IUnitOfwork unitOfwork;
        private readonly IFileAssignementAnswerProcessing fileAssignementAnswerProcessing;

        public UploadFileToAssignementAnswerHandler(IUnitOfwork unitOfwork, IFileAssignementAnswerProcessing fileAssignementAnswerProcessing)
        {
            this.unitOfwork = unitOfwork;
            this.fileAssignementAnswerProcessing = fileAssignementAnswerProcessing;
        }

        public async Task<Result<string>> Handle(UploadFileToAssignementAnswerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                #region GetAllIdForRoute
                int AssignementAnswerId, AssignementId, SectionId, CourseCycleId, AcadimicYearId, DepartementId, FacultyId;
              
                var AssignementAnswer= await unitOfwork.AssignementAnswerRepository.GetByIdAsync(request.UploadFileToAssignementAnswerDto.AssignementAnswerId);  
              
                if(AssignementAnswer is null || AssignementAnswer.AssignmentId!=request.UploadFileToAssignementAnswerDto.AssignementId)
                    return Result.Failure<string>(new Error("UploadFileToAssignementAnswer", "This AssignementAnswer is not belong to any Section"));

                AssignementAnswerId = AssignementAnswer.AssignmentAnswer_id;

                var Assignement = await unitOfwork.AssignementRepository.GetByIdAsync(request.UploadFileToAssignementAnswerDto.AssignementId);

                if (Assignement is null )
                    return Result.Failure<string>(new Error("UploadFileToAssignementAnswer", "This AssignementAnswer is not belong to any Section"));

                AssignementId = request.UploadFileToAssignementAnswerDto.AssignementId;
                SectionId = Assignement.SectionId;

                var Section = await unitOfwork.SectionRepository.GetByIdAsync(SectionId);

                if (Section is null)
                    return Result.Failure<string>(new Error("UploadFileToAssignement", "The Section has this assignemnetAnswer is deleted"));

                CourseCycleId = Section.CourseCycleId;

                var CourseCycle = await unitOfwork.CourseCycleRepository.GetByIdAsync(CourseCycleId);

                if (CourseCycle is null)
                    return Result.Failure<string>(new Error("UploadFileToAssignementAnswer", "This Course or Group having this Section may be deleted"));

                var AcadimicYear = await unitOfwork.AcadimicYearRepository.GetAcadimicYearHasSpecificCourseIdAndGroupAsync(courseId: CourseCycle.CourseId, groupId: CourseCycle.GroupId);

                if (AcadimicYear is null)
                    return Result.Failure<string>(new Error("UploadFileToAssignementAnswer", "The Acadimic Year contain this Course or Group may be deleted"));

                AcadimicYearId = AcadimicYear.AcadimicYearId;
                DepartementId = AcadimicYear.DepartementId;

                var Departement = await unitOfwork.DepartementRepository.GetByIdAsync(DepartementId);

                if (Departement is null)
                    return Result.Failure<string>(new Error("UploadFileToAssignementAnswer", "The Departement has this Acadimic Year may be deleted"));
                FacultyId = Departement.FacultyId;
                #endregion

                Result<string> resultOfUploadingFile = await fileAssignementAnswerProcessing.UploadFileForAssignementAnswer(request.UploadFileToAssignementAnswerDto,
                                FacultyId, DepartementId, AcadimicYearId, CourseCycleId, SectionId, AssignementId,AssignementAnswerId);


                if (resultOfUploadingFile is not null && resultOfUploadingFile.IsSuccess)
                    return Result.Success<string>(resultOfUploadingFile.Value);
                return Result.Failure<string>(new Error("UploadFileToAssignementAnswer", "There is a probelm"));

            }
            catch (Exception ex)
            {
                return Result.Failure<string>(new Error("UploadFileToAssignementAnswer", "There is a probelm"));
            }
        }
    }
}
