using Application.CQRS.Command.FileResources;
using Application.CQRS.Command.FilesProcessing;
using Application.CQRS.Query.AssignementAnswers;
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
 //   [Authorize]

    [Route("api/[controller]")]
    [ApiController]
    public class FileProcessingOfAssignementAnswerController : ControllerBase
    {
        private readonly IMediator mediator;
        string hostUrl = "";// = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
        public FileProcessingOfAssignementAnswerController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpPost("UploadFileToAssignementAnswer")]
        public async Task<ActionResult> UploadSingleFile([FromForm] UploadFileToAssignementAnswerDto addFileToAssignementAnswerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                hostUrl = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
                var command = new UploadFileToAssignementAnswerCommand { UploadFileToAssignementAnswerDto = addFileToAssignementAnswerDto };
                var result = await mediator.Send(command);
                if (result is not null && result.IsSuccess)
                {
                    hostUrl += "//";
                    hostUrl += result.Value;

                    var commandAddAssignementAnswerResource = new CreateFileResourceCommand
                    {
                        TypeOfEntity = EntitiesHasFiles.AssignementAnswer,

                        FileResourceDto = new FileResourceDto
                        {
                            Name = addFileToAssignementAnswerDto.Name,
                            Url = hostUrl,
                            FileType = addFileToAssignementAnswerDto.FileType,
                            AssignmentAnswerId = addFileToAssignementAnswerDto.AssignementAnswerId
                        }
                    };
                    var resultOfAddAssignementAnswerResource = await mediator.Send(commandAddAssignementAnswerResource);

                    if (resultOfAddAssignementAnswerResource is not null && resultOfAddAssignementAnswerResource.IsSuccess)
                        return Ok(hostUrl);
                }
                return BadRequest("Failed To upload the File");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("GetAssignementAnswerFiles")]
        public async Task<ActionResult> GetFilesOfAssignementAnswer([FromHeader] int AssignmentId ,  string StudentUserName)
        {
            try
            {
                if (AssignmentId == 0 || StudentUserName=="")
                    return BadRequest("Enter valid AssignementAnswerId");

                var ResultOfAssignmnetAnswer= await mediator.Send(new  GetAssignmentAnswerIdQuery { AssessmentId=AssignmentId,StudentUserName=StudentUserName});
                if (ResultOfAssignmnetAnswer is null || ResultOfAssignmnetAnswer.IsFailure)
                    return BadRequest("Invalid data");

                
                var resultOfFilesUrlOfAssignementAnswer = await mediator.Send(new GetAllFilesOfEntityHasFilesQuery
                {
                    TypeOfEntity = EntitiesHasFiles.AssignementAnswer,
                    AssignmentAnswerId = ResultOfAssignmnetAnswer.Value
                });
                if (resultOfFilesUrlOfAssignementAnswer is not null && resultOfFilesUrlOfAssignementAnswer.IsSuccess)
                    return Ok(resultOfFilesUrlOfAssignementAnswer.Value);

                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }



        [HttpPost("UploadMultiplyFiles")]
        public async Task<ActionResult> UploadMultiplyFiles([FromForm] List<UploadFileToAssignementAnswerDto> addFileToAssignementAnswerDtos)
        {
            List<string> returnedList = new List<string>();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            foreach (var addFileToAssignementAnswerDto in addFileToAssignementAnswerDtos)
            {
                try
                {
                    hostUrl = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
                    var command = new UploadFileToAssignementAnswerCommand { UploadFileToAssignementAnswerDto = addFileToAssignementAnswerDto };
                    var result = await mediator.Send(command);
                    if (result is not null && result.IsSuccess)
                    {
                        hostUrl += "//";
                        hostUrl += result.Value;

                        var commandAddAssignementAnswerResource = new CreateFileResourceCommand
                        {
                            TypeOfEntity = EntitiesHasFiles.AssignementAnswer,

                            FileResourceDto = new FileResourceDto
                            {
                                Name = addFileToAssignementAnswerDto.Name,
                                Url = hostUrl,
                                FileType = addFileToAssignementAnswerDto.FileType,
                                AssignmentAnswerId = addFileToAssignementAnswerDto.AssignementAnswerId
                            }
                        };
                        var resultOfAddAssignementAnswerResource = await mediator.Send(commandAddAssignementAnswerResource);

                        if (resultOfAddAssignementAnswerResource is not null && resultOfAddAssignementAnswerResource.IsSuccess)
                            returnedList.Add(hostUrl);

                    }
                    returnedList.Add($"Can not upload file with name {addFileToAssignementAnswerDto.Name} and extention {addFileToAssignementAnswerDto.FileType.ToString()}");
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
