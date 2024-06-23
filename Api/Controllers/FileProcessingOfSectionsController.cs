using Application.CQRS.Command.FileResources;
using Application.CQRS.Command.FilesProcessing;
using Application.CQRS.Command.LectureResourses;
using Application.CQRS.Query.FileResources;
using Application.enums;
using Contract.Dto.FileResources;
using Contract.Dto.Files;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Authorize]

    [Route("api/[controller]")]
    [ApiController]
    public class FileProcessingOfSectionsController : ControllerBase
    {
        private readonly IMediator mediator;
        string hostUrl = "";// = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
        public FileProcessingOfSectionsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("UploadFileToSection")]
        public async Task<ActionResult> UploadSingleFile([FromForm] UploadFileToSectionDto addFileToSectionDto )
        {
            if (! ModelState.IsValid)
                return BadRequest("Enter Valid LectureId");
            try
            {
                hostUrl = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
                var command = new UploadFileToSectionCommand { uploadFileToSectionDto = addFileToSectionDto };
                var result = await mediator.Send(command);

                if(result is not null && result.IsSuccess)
                {
                    hostUrl += "//";
                    hostUrl += result.Value;
                    var commandAddLectureResource = new CreateFileResourceCommand
                    {
                        TypeOfEntity = EntitiesHasFiles.Lecture,

                        FileResourceDto = new FileResourceDto
                        {
                            Name = addFileToSectionDto.Name,
                            Url = hostUrl,
                            FileType = addFileToSectionDto.FileType,
                            LectureId = addFileToSectionDto.LectureId
                        }
                    };
                    var resultOfAddLectureResource = await mediator.Send(commandAddLectureResource);

                    if (resultOfAddLectureResource is not null && resultOfAddLectureResource.IsSuccess)
                        return Ok(hostUrl);
                }
                return BadRequest("Failed To upload the File");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpGet("GetSectionFiles")]
        public async Task<ActionResult> GetFilesOfCourse([FromHeader] int LectureId)
        {
            try
            {
                if (LectureId == 0)
                    return BadRequest("Enter valid LectureId");
                var resultOfFilesUrlOfCourse = await mediator.Send(new GetAllFilesOfEntityHasFilesQuery
                {
                    TypeOfEntity=EntitiesHasFiles.Lecture,
                    LectureId=LectureId
                });
                if (resultOfFilesUrlOfCourse is not null && resultOfFilesUrlOfCourse.IsSuccess)
                    return Ok(resultOfFilesUrlOfCourse.Value);

                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

    }
}