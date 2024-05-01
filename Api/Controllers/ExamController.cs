using Application.CQRS.Command.Exams;
using Application.CQRS.Command.StudentExams;
using Application.CQRS.Query.Exams;
using Application.CQRS.Query.StudentExams;
using Contract.Dto.Exams;
using Contract.Dto.StudentExamDto;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamController : ControllerBase
    {
        private readonly IMediator mediator;
        public ExamController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("CreateExam")]
        public async  Task<ActionResult> CreateExam (ExamDto examDto)
        {
            if(!ModelState.IsValid)
             return BadRequest(ModelState);
            var ResultOfCreateExam = await mediator.Send(new CreateExamCommand { ExamDto = examDto });

            if (ResultOfCreateExam is not null && ResultOfCreateExam.IsSuccess)
                return Ok(ResultOfCreateExam.Value) ;

            return BadRequest(ResultOfCreateExam?.Value);            
        }

        [HttpDelete("DeleteExam")]
        public async Task<ActionResult> DeleteExam([FromHeader] int ExamId)
        {
            if (ExamId == 0)
                return BadRequest("invalid Id");

            var ResultOfDelete=await mediator.Send(new DeleteExamCommand { ExamId = ExamId });
            if(ResultOfDelete is not null &&ResultOfDelete.IsSuccess)
                return Ok(ResultOfDelete.Value) ;
            return BadRequest(ResultOfDelete?.Error) ;
        }
       

        [HttpGet("GetExamsOfSectionToInstuructor")]

        public async Task<ActionResult> GetExamsOfSectionToInstuructorOnSection([FromHeader] string InstructorUserName, [FromHeader] int SectionId)
        {
            if (SectionId == 0 || InstructorUserName=="")
                return BadRequest("invalid Id");

            var Result = await mediator.Send(new GetAllQuizesOfSectionOfInstructorQuery { InstructorUserName = InstructorUserName, SectionId = SectionId });

            if (Result.IsSuccess)
            {
                return Ok(Result.Value) ;
            }
            return BadRequest(Result.Error) ;
        }


        [HttpPost("SubmitExamToStudent")]
        public async Task<ActionResult> SubmitExamToStudent(StudentSubmitionOfExamDto studentAnswersOfExamDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var ResultOfSubmition =await mediator.Send(new SubmitExamToStudentCommand
            {
                studentAnswersOfExamDto = studentAnswersOfExamDto
            });

            if (ResultOfSubmition is not null && ResultOfSubmition.IsSuccess)
                return Ok(ResultOfSubmition.Value);

            return BadRequest();

        }

        [HttpGet("GetAllExamWorkNowToStudent")]
        public async Task<ActionResult> GetExamWorkNowToStudent(int ExamId , string StudnetUserName)
        {
            if (ExamId == 0 || StudnetUserName == "")
                return BadRequest("invalid Id");
            var Result = await mediator.Send(new GetExamWorkNowToStudentCommand { StudentUserName = StudnetUserName, ExamId = ExamId });

            if (Result.IsSuccess)
            {
                return Ok(Result.Value);
            }
            return BadRequest(Result.Error);
        }

        [HttpGet("GetStudentanswerOfExamWithModelAnswer")]
        public async Task<ActionResult> GetStudentanswerOfExamWithModelAnswer (int ExamId, string StudnetUserName)
        {
            if (ExamId == 0 || StudnetUserName == "")
                return BadRequest("invalid Id");
            var Result = await mediator.Send(new GetExamAnswerOfStudentQuery { StudentUserName = StudnetUserName, ExamId = ExamId });

            if (Result.IsSuccess)
            {
                return Ok(Result.Value);
            }
            return BadRequest(Result.Error);
        }
    }
}
