
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
    public class FileProcessingOfCoursesController : ControllerBase
    {
        private readonly IMediator mediator;
        string hostUrl = "";// = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
        public FileProcessingOfCoursesController( IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpPost("UploadFileToCourse")]
        public async Task<ActionResult> UploadSingleFile([FromForm] UploadFileToCourseDto addFileToCourseDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                hostUrl = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
                var command = new UploadFileToCourseCommand { uploadFileToCourseDto = addFileToCourseDto };
                var result = await mediator.Send(command);
                if (result is not null && result.IsSuccess)
                {
                    hostUrl += "//";
                    hostUrl += result.Value;

                    var commandAddLectureResource = new CreateFileResourceCommand
                    {
                        TypeOfEntity=EntitiesHasFiles.Lecture,

                        FileResourceDto = new FileResourceDto
                        {
                            Name = addFileToCourseDto.Name,
                            Url = hostUrl,
                            FileType = addFileToCourseDto.FileType,
                            LectureId = addFileToCourseDto.LectureId
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

        [HttpGet("GetCourseFiles")]
        public async Task<ActionResult> GetFilesOfCourse([FromHeader] int LectureId)
        {
            try
            {
                if (LectureId == 0)
                    return BadRequest("Enter valid LectureId");
                var resultOfFilesUrlOfCourse = await mediator.Send(new GetAllFilesOfEntityHasFilesQuery
                {
                    TypeOfEntity=EntitiesHasFiles.Lecture,
                    LectureId= LectureId
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



        [HttpPost("UploadMultiplyFiles")]
        public async Task<ActionResult> UploadMultiplyFiles([FromForm] List<UploadFileToCourseDto> addFileToCourseDtos)
        {
            List<string> returnedList = new List<string>();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            foreach (var addFileToCourseDto in addFileToCourseDtos)
            {
                try
                {
                    hostUrl = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
                    var command = new UploadFileToCourseCommand { uploadFileToCourseDto = addFileToCourseDto };
                    var result = await mediator.Send(command);
                    if (result is not null && result.IsSuccess)
                    {
                        hostUrl += "//";
                        hostUrl += result.Value;

                        var commandAddLectureResource = new CreateFileResourceCommand
                        {
                            TypeOfEntity = EntitiesHasFiles.Lecture,

                            FileResourceDto = new FileResourceDto
                            {
                                Name = addFileToCourseDto.Name,
                                Url = hostUrl,
                                FileType = addFileToCourseDto.FileType,
                                LectureId = addFileToCourseDto.LectureId
                            }
                        };
                        var resultOfAddLectureResource = await mediator.Send(commandAddLectureResource);

                        if (resultOfAddLectureResource is not null && resultOfAddLectureResource.IsSuccess)
                            returnedList.Add(hostUrl);

                    }
                    returnedList.Add($"Can not upload file with name {addFileToCourseDto.Name} and extention {addFileToCourseDto.FileType.ToString()}");
                }
                catch (Exception ex)
                {
                    return BadRequest(returnedList);
                }
            }

            return Ok(returnedList);
        }
    }
}
