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
    public class UploadFileToAssignementHandler : ICommandHandler<UploadFileToAssignementCommand, string>
    {
        private readonly IUnitOfwork unitOfwork;
        private readonly IFileAssignementProcessing fileAssignementProcessing;

        public UploadFileToAssignementHandler(IUnitOfwork unitOfwork, IFileAssignementProcessing fileAssignementProcessing)
        {
            this.unitOfwork = unitOfwork;
            this.fileAssignementProcessing = fileAssignementProcessing;
        }

        public async Task<Result<string>> Handle(UploadFileToAssignementCommand request, CancellationToken cancellationToken)
        {
            try
            {
                #region GetAllIdForRoute
                int AssignementId, SectionId, CourseCycleId, AcadimicYearId, DepartementId, FacultyId;
                var Assignement = await unitOfwork.AssignementRepository.GetByIdAsync(request.UploadFileToAssignementDto.AssignementId);

                if (Assignement is null || Assignement.SectionId != request.UploadFileToAssignementDto.SectionId)
                    return Result.Failure<string>(new Error("UploadFileToAssignement", "This Assignement is not belong to any Section"));

                AssignementId = request.UploadFileToAssignementDto.AssignementId;
                SectionId = request.UploadFileToAssignementDto.SectionId;

                var Section = await unitOfwork.SectionRepository.GetByIdAsync(SectionId);

                if (Section is null)
                    return Result.Failure<string>(new Error("UploadFileToAssignement", "The Section has this assignemnet is deleted"));

                CourseCycleId = Section.CourseCycleId;

                var CourseCycle = await unitOfwork.CourseCycleRepository.GetByIdAsync(CourseCycleId);

                if (CourseCycle is null)
                    return Result.Failure<string>(new Error("UploadFileToAssignement", "This Course or Group having this Section may be deleted"));

                var AcadimicYear = await unitOfwork.AcadimicYearRepository.GetAcadimicYearHasSpecificCourseIdAndGroupAsync(courseId: CourseCycle.CourseId, groupId: CourseCycle.GroupId);

                if (AcadimicYear is null)
                    return Result.Failure<string>(new Error("UploadFileToAssignement", "The Acadimic Year contain this Course or Group may be deleted"));

                AcadimicYearId = AcadimicYear.AcadimicYearId;
                DepartementId = AcadimicYear.DepartementId;

                var Departement = await unitOfwork.DepartementRepository.GetByIdAsync(DepartementId);

                if (Departement is null)
                    return Result.Failure<string>(new Error("UploadFileToAssignement", "The Departement has this Acadimic Year may be deleted"));
                FacultyId = Departement.FacultyId;
                #endregion

                Result<string> resultOfUploadingFile = await fileAssignementProcessing.UploadFileForAssignement(request.UploadFileToAssignementDto,
                                FacultyId, DepartementId, AcadimicYearId, CourseCycleId, SectionId, AssignementId);


                if (resultOfUploadingFile is not null && resultOfUploadingFile.IsSuccess)
                    return Result.Success<string>(resultOfUploadingFile.Value);
                return Result.Failure<string>(new Error("UploadFileToAssignement", "There is a probelm"));

            }
            catch (Exception ex)
            {
                return Result.Failure<string>(new Error("UploadFileToAssignement", "There is a probelm"));
            }
        }
    }
}
