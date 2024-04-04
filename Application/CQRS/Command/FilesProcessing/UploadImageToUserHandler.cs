using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.FileProcessing;
using Application.Common.Interfaces.Presistance;
using Contract.Dto.Files;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Command.FilesProcessing
{
    public class UploadImageToUserHandler : ICommandHandler<UploadImageToUserCommand, string>
    {
        private readonly IUnitOfwork unitOfwork;
        private readonly IFileImageProcessing fileImageProcessing;

        public UploadImageToUserHandler(IUnitOfwork unitOfwork, IFileImageProcessing fileImageProcessing)
        {
            this.unitOfwork = unitOfwork;
            this.fileImageProcessing = fileImageProcessing;
        }

        public async Task<Result<string>> Handle(UploadImageToUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.UploadImageToUserDto.FileType != Domain.TmpFilesProcessing.FileType.JPG && request.UploadImageToUserDto.FileType != Domain.TmpFilesProcessing.FileType.PNG)
                    return Result.Failure<string>(new Error("UploadImageToUser", "Enter valid file extention"));

                fileImageProcessing.SetTypeOfUser(request.UploadImageToUserDto.TypeOfUser);
                Result<string> resultOfUploadImage = await fileImageProcessing.UploadFileImageToUser(request.UploadImageToUserDto);
                if (resultOfUploadImage is null || resultOfUploadImage.IsFailure)
                    return Result.Failure<string>(new Error("UploadImageToUser", "Unable To Add photo"));

                return Result.Success<string>(resultOfUploadImage.Value);
            }
            catch (Exception ex)
            {
                return Result.Failure<string>(new Error("UploadImageToUser", "Unable To Add photo"));
            }
        }
    }
}
