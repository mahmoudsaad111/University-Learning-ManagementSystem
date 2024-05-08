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
        public async Task<ActionResult> DeleteExam([FromHeader] int ExamId , [FromBody] string ExamCreatorUserName)
        {
            if (ExamId == 0)
                return BadRequest("invalid Id");

            var ResultOfDelete=await mediator.Send(new DeleteExamCommand { ExamId = ExamId , ExamCreatorUserName= ExamCreatorUserName });
            if(ResultOfDelete is not null &&ResultOfDelete.IsSuccess)
                return Ok(ResultOfDelete.Value) ;
            return BadRequest(ResultOfDelete?.Error) ;
        }
        [HttpPut("UpdateExam")]
        public async Task<ActionResult> UpdateExam([FromBody] UpdateExamDto updateExamDto , [FromHeader] int ExamId, [FromQuery] string ExamCreatorUserName)
        {
            var ResultOfUpdated = await mediator.Send(new UpdateExamCommand { UpdateExamDto = updateExamDto, ExamCreatorUserName = ExamCreatorUserName, ExamId = ExamId });

            if (ResultOfUpdated is not null  &&ResultOfUpdated.IsSuccess)
                return Ok(ResultOfUpdated.Value) ;
            return BadRequest(ResultOfUpdated?.Error);
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



        [HttpGet("GetExamsOfCourseCycleToProfessor")]

        public async Task<ActionResult> GetExamsOfCourseCycleToProfessor([FromHeader] string ProfessorUserName, [FromHeader] int CourseCycleId)
        {
            if (CourseCycleId == 0 || ProfessorUserName == "")
                return BadRequest("invalid Id");

            var Result = await mediator.Send(new GetAllExamsOfCourseCycleForProfessorQuery { ProfessorUserName = ProfessorUserName, CourseCycleId = CourseCycleId });

            if (Result.IsSuccess)
            {
                return Ok(Result.Value);
            }
            return BadRequest(Result.Error);
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
            var Result = await mediator.Send(new GetExamWorkNowToStudentQuery { StudentUserName = StudnetUserName, ExamId = ExamId });

            if (Result.IsSuccess)
            {
                return Ok(Result.Value);
            }
            return BadRequest(Result.Error);
        }

        [HttpGet("GetAllQuestionsOfExam")]
        public async Task<ActionResult> GetAllQuestionsOfExam (int ExamId, string ExamCreatorUserName)
        {
            if (ExamId == 0 || ExamCreatorUserName == "")
                return BadRequest("invalid Id");
            var Result = await mediator.Send(new GetAllQuestionsOfExamQuery { ExamCreatorUserName = ExamCreatorUserName, ExamId = ExamId });
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

        [HttpGet("GetAllQuizesOfSectionToStudent")]
        public async Task<ActionResult> GetAllQuizesOfSectionToStudent(int sectionId, string studentUserName)
        {
            if (sectionId == 0 || studentUserName == "")
                return BadRequest("Invalid data");
            var Result = await mediator.Send(new GetAllQuizessOfStudentOnSectionQuery { StudentUserName = studentUserName, SectionId=sectionId });

            if (Result.IsSuccess)
            {
                return Ok(Result.Value);
            }
            return BadRequest(Result.Error);
        }

        [HttpGet("GetAllExamsOfCourseCycleToStudent")]
        public async Task<ActionResult> GetAllExamsOfCourseCycleToStudent(int courseCycleId, string studentUserName)
        {
            if (courseCycleId == 0 || studentUserName == "")
                return BadRequest("Invalid data");
            var Result = await mediator.Send(new GetAllExamsOfCourseCycleForStudentQuery { StudentUserName = studentUserName, CourseCycleId = courseCycleId });

            if (Result.IsSuccess)
            {
                return Ok(Result.Value);
            }
            return BadRequest(Result.Error);
        }


        [HttpGet("GetAllStudnetsAttendExam")]
        public async Task<ActionResult> GetAllStudnetsAttendExam(int ExamId, string ExamCreatorUserName)
        {
            if (ExamId == 0 || ExamCreatorUserName == "")
                return BadRequest("Invalid data");
            var Result = await mediator.Send(new GetAllStudentsAttendExamQuery {  ExamId=ExamId , ExamCreatorUserName= ExamCreatorUserName });

            if (Result.IsSuccess)
            {
                return Ok(Result.Value);
            }
            return BadRequest(Result.Error);
        }
    }
}
