using Application.CQRS.Command.FileResources;
using Application.CQRS.Command.FilesProcessing;
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
    public class FileProcessingOfAssignementController : ControllerBase
    {
        private readonly IMediator mediator;
        string hostUrl = "";// = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
        public FileProcessingOfAssignementController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpPost("UploadFileToAssignement")]
        public async Task<ActionResult> UploadSingleFile([FromForm] UploadFileToAssignementDto addFileToAssignementDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                hostUrl = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
                var command = new UploadFileToAssignementCommand { UploadFileToAssignementDto = addFileToAssignementDto };
                var result = await mediator.Send(command);
                if (result is not null && result.IsSuccess)
                {
                    hostUrl += "//";
                    hostUrl += result.Value;

                    var commandAddAssignementResource = new CreateFileResourceCommand
                    {
                        TypeOfEntity = EntitiesHasFiles.Assignement,

                        FileResourceDto = new FileResourceDto
                        {
                            Name = addFileToAssignementDto.Name,
                            Url = hostUrl,
                            FileType = addFileToAssignementDto.FileType,
                            AssignmentId = addFileToAssignementDto.AssignementId
                        }
                    };
                    var resultOfAddAssignementResource = await mediator.Send(commandAddAssignementResource);

                    if (resultOfAddAssignementResource is not null && resultOfAddAssignementResource.IsSuccess)
                        return Ok(hostUrl);
                }
                return BadRequest("Failed To upload the File");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("GetAssignementFiles")]
        public async Task<ActionResult> GetFilesOfAssignement([FromHeader] int AssignementId)
        {
            try
            {
                if (AssignementId == 0)
                    return BadRequest("Enter valid AssignementId");
                var resultOfFilesUrlOfAssignement = await mediator.Send(new GetAllFilesOfEntityHasFilesQuery
                {
                    TypeOfEntity = EntitiesHasFiles.Assignement,
                    AssignmentId = AssignementId
                });
                if (resultOfFilesUrlOfAssignement is not null && resultOfFilesUrlOfAssignement.IsSuccess)
                    return Ok(resultOfFilesUrlOfAssignement.Value);

                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }



        [HttpPost("UploadMultiplyFiles")]
        public async Task<ActionResult> UploadMultiplyFiles([FromForm] List<UploadFileToAssignementDto> addFileToAssignementDtos)
        {
            List<string> returnedList = new List<string>();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            foreach (var addFileToAssignementDto in addFileToAssignementDtos)
            {
                try
                {
                    hostUrl = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
                    var command = new UploadFileToAssignementCommand { UploadFileToAssignementDto = addFileToAssignementDto };
                    var result = await mediator.Send(command);
                    if (result is not null && result.IsSuccess)
                    {
                        hostUrl += "//";
                        hostUrl += result.Value;

                        var commandAddAssignementResource = new CreateFileResourceCommand
                        {
                            TypeOfEntity = EntitiesHasFiles.Assignement,

                            FileResourceDto = new FileResourceDto
                            {
                                Name = addFileToAssignementDto.Name,
                                Url = hostUrl,
                                FileType = addFileToAssignementDto.FileType,
                                AssignmentId = addFileToAssignementDto.AssignementId
                            }
                        };
                        var resultOfAddAssignementResource = await mediator.Send(commandAddAssignementResource);

                        if (resultOfAddAssignementResource is not null && resultOfAddAssignementResource.IsSuccess)
                            returnedList.Add(hostUrl);

                    }
                    returnedList.Add($"Can not upload file with name {addFileToAssignementDto.Name} and extention {addFileToAssignementDto.FileType.ToString()}");
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
