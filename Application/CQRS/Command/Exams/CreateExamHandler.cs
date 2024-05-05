using Application.Common.Interfaces.CQRSInterfaces;
using Application.Common.Interfaces.Presistance;
using Domain.Models;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;
using System.Diagnostics.Eventing.Reader;


namespace Application.CQRS.Command.Exams
{
    public class CreateExamHandler : ICommandHandler<CreateExamCommand, Exam>
    {
        private readonly IUnitOfwork unitOfwork;

        public CreateExamHandler(IUnitOfwork unitOfwork)
        {
            this.unitOfwork = unitOfwork;
        }
 
        public async Task<Result<Exam>> Handle(CreateExamCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.ExamDto.SartedAt < DateTime.Now)
                    return Result.Failure<Exam>(new Error(code: "Create Exam", message: "Can not create exam with this data time"));


                var ExamDto = request.ExamDto; // type of ExamDto
                Exam ExamWillbeAdded= ExamDto.GetExam();
              
                ExamPlace ValidExamPalce = null;
                bool IfValidExamPalceExist = false; // To insure that combination between foreign keys and ExamType i correct for Example (ExamType.Semester && CourseId=0 && sectionId!=0) refused


                //check if examPlace for course (semester or final type) exist or not
                //if not exist create one and get Id 
                var TupleOfBoolAndExamPlace = await unitOfwork.ExamRepository.CheckIfExamDataValidHasValidExamPlace(ExamDto);
                /*
                 * " CheckIfExamDataValidHasValidExamPlace " this function has three cases
                 * case 1: if the data is valid and this exam place is not exist it well return (false , exam place)
                 * case 2 :if the data is valid and the exam place is exist it well return (true , exam place)
                 * case 3: if data is no valid return (false ,null)
                 */

                IfValidExamPalceExist = TupleOfBoolAndExamPlace.Item1;
                ValidExamPalce = TupleOfBoolAndExamPlace.Item2;


                if (ValidExamPalce is not null  && !IfValidExamPalceExist )
                {
                    ValidExamPalce = await unitOfwork.ExamPlaceRepository.CreateAsync(ValidExamPalce);
                    await unitOfwork.SaveChangesAsync();

                    IfValidExamPalceExist = true; 
                }

                if (IfValidExamPalceExist) 
                {
                    bool IfUserHasAccessToExam = false;
                    if(ExamDto.ExamType==ExamType.Quiz && ExamDto.SectionId != 0)
                    {
                        var instructor = await unitOfwork.UserRepository.GetUserByUserName(request.ExamDto.InstructorUserName);
                        if (instructor is not null)
                            IfUserHasAccessToExam = await unitOfwork.SectionRepository.CheckIfInstructorInSection(InstrucotrId: instructor.Id,SectionId:  ExamDto.SectionId);
                    }
                    else if(ExamDto.ExamType==ExamType.Quiz && ExamDto.CourseCycleId != 0)
                    {
                        var professor = await unitOfwork.UserRepository.GetUserByUserName(request.ExamDto.ProfessorUserName);
                        if (professor is not null)
                            IfUserHasAccessToExam = await unitOfwork.CourseCycleRepository.CheckIfProfInCourseCycle(ProfId: professor.Id, CourseCycleId:  ExamDto.CourseCycleId);

                    }
                    else if(ExamDto.ExamType==ExamType.Midterm && ExamDto.CourseCycleId != 0)
                    {
                        var professor = await unitOfwork.UserRepository.GetUserByUserName(request.ExamDto.ProfessorUserName);
                        if (professor is not null)
                            IfUserHasAccessToExam = await unitOfwork.CourseCycleRepository.CheckIfProfInCourseCycle(ProfId: professor.Id, CourseCycleId: ExamDto.CourseCycleId);
                    }
                    else if  (ExamDto.ExamType==ExamType.Semester || ExamDto.ExamType==ExamType.Final)
                    {
                        IfUserHasAccessToExam = true; 
                        // there is no validation here, but the correct thing is to check if the Stuff has access or not but this is not implemented
                    }


                    if (IfUserHasAccessToExam)
                    {
                        ExamWillbeAdded.ExamPlaceId = ValidExamPalce.ExamPlaceId;
                        ExamWillbeAdded.FullMark = 0; // well computed as the marks of each Question ; 
                        Exam ExamAdded = await unitOfwork.ExamRepository.CreateAsync(ExamWillbeAdded);

                        if (ExamAdded is not null)
                        {
                            await unitOfwork.SaveChangesAsync();
                            return Result.Success<Exam>(ExamAdded);
                        }

                        return Result.Failure<Exam>(new Error(code: "Create Exam", message: "Can not added the exam"));
                    }

                    return Result.Failure<Exam>(new Error(code: "Create Exam", message: "The user has no access"));
                }

                return Result.Failure<Exam>(new Error(code: "Create Exam", message: "Can not create exam with this data"));


            }
            catch (Exception ex)
            {
                return Result.Failure<Exam>(new Error(code: "Create Exam", message: ex.Message.ToString())) ;
            }
        }
    }
}
